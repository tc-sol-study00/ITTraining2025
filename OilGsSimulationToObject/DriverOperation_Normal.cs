using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {
    internal class DriverOperation_Normal {

        private static List<JidoSha> JidoShas = new List<JidoSha>();
        private static GsStation gsStation = new GsStation();
        public DriverOperation_Normal() {

            JidoShas.Add(new JidoSha("A車", 20, 60, 0.2));
            JidoShas.Add(new JidoShawithOil("B車", 20, 60, 0.2, 0));

            foreach (JidoSha aCar in JidoShas) {
                /*
                 * 走行距離入力で、"E"あるいは"e"が入力されるまで処理を継続する
                 * ガス欠の場合は、処理終了
                 */
                Console.WriteLine("{0}のシミュレーション", aCar.CarName);

                while (true) {
                    /*
                     * 自宅
                     */

                    // 走行

                    //走行距離入力
                    Console.Write("走行距離=");
                    string? strSoKouKyori = Console.ReadLine();
                    if (strSoKouKyori == "e" || strSoKouKyori == "E") break;
                    int? souKouKyori = Int32.Parse(strSoKouKyori);

                    //走行距離インプット・タンク残量計算
                    if (aCar is JidoShawithOil jidoShaWithOil) {
                       
                        jidoShaWithOil.Soukou(souKouKyori ?? 0);
                    }
                    else if (aCar is JidoSha jidoSha) {
                        aCar.Soukou(souKouKyori ?? 0);
                    }

                    if (aCar.GasKetsuCheck()) break;   //ガス欠チェック
                    /*
                     * チェック
                     */
                    //残量チェック　（OK=true/NG=false)
                    bool checkStatus = aCar.GsRemainCheck();

                    //オイル交換時期チェック（OK=true/NG=false)
                    //自動車クラスの場合は、オイル交換時期は問題なし扱い
                    bool oilStatus = true;
                    if (aCar is JidoShawithOil jidoShawithOil) {
                        oilStatus = jidoShawithOil.OilCheck();
                    }

                    //上記２つのチェックがＯＫであれば、次の走行へ
                    if (checkStatus && oilStatus) continue;

                    /*
                     * ＧＳへ行く？
                     */
                    bool flgDisplayed = false;
                    if (!oilStatus) {
                        Console.WriteLine("オイル交換必要です。走行距離={0:F2}, 次回交換時期={1:F2} ", ((JidoShawithOil)aCar).SouSoukouKyori, ((JidoShawithOil)aCar).SouSoukouKyoriAtNextOilChange);
                        flgDisplayed = true;
                    }
                    Console.Write("給油しますか？(y/n)=");
                    var strFuelWillness = Console.ReadLine();
                    if (strFuelWillness == "y") {   //GSに行くと指示された
                                                    //ＧＳへ
                        if (aCar is JidoShawithOil jidoShawithOil2) {
                            oilStatus = jidoShawithOil2.OilCheck();
                            jidoShawithOil2.RunBetweenHomeToGs(5);
                        }
                        else {
                            aCar.RunBetweenHomeToGs(5);
                        }
                        if (aCar.GasKetsuCheck()) break;

                        /*
                         * GS
                         */
                        //満タン
                        //必要な供給量
                        gsStation.FuelCharge(aCar);

                        if (aCar is JidoShawithOil jidoShaWithOil3) {
                            //オイルチェック
                            if (!jidoShaWithOil3.OilCheck()) {
                                if (!flgDisplayed) Console.WriteLine("オイル交換必要です。");
                                //オイル交換
                                gsStation.OilCharge(jidoShaWithOil3);
                            }
                        }
                        //家に戻る
                        aCar.RunBetweenGsToHome(5);
                        if (aCar.GasKetsuCheck()) break;
                    }

                }

            }
        }
    }
}
