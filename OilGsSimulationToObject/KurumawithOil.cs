using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OilGsSimulationToObject {
    public class KurumawithOil : Kuruma, IKurumawithOil {

        public double SouSoukouKyori { get; set; }
        public double OilChangeInterval { get; set; }
        public double SouSoukouKyoriAtLastOilChange { get; set; }
        public double SouSoukouKyoriAtNextOilChange { get; set; }

        public KurumawithOil(string inCarName, double inNenPi, double inTankFull, double inLimitPct, double inSouSoukouKyori) : base(inCarName, inNenPi, inTankFull, inLimitPct) {
            SouSoukouKyori = inSouSoukouKyori;        //総走行距離（新車納品時は0とする）
            //オイル交換関係変数
            OilChangeInterval = 5000.0;
            SouSoukouKyoriAtLastOilChange = SouSoukouKyori;              //前回のオイル交換時の総走行距離
            SouSoukouKyoriAtNextOilChange = SouSoukouKyori + OilChangeInterval;  //次回のオイル交換時の総走行距離
        }
        public void GsOilSimulation() {
            while (true) {

                //走行距離　（残量）

                (TankZanryo, double? soukouKyori, SouSoukouKyori) = Travel(NenPi, TankZanryo);   //走行距離インプット・タンク残量計算
                if (soukouKyori == null) break;

                if (GasKetsuCheck(TankZanryo)) break;   //ガス欠チェック

                //残量チェック　（OK=true/NG=false)
                bool checkStatus = GsRemainCheck(TankZanryo, TankLimit);
                //オイル交換時期チェック（OK=true/NG=false)
                bool oilStatus = OilCheck(SouSoukouKyori, SouSoukouKyoriAtNextOilChange);

                if (checkStatus && oilStatus) continue;

                bool flgDisplayed = false;

                if (!oilStatus) {
                    Console.WriteLine("オイル交換必要です。走行距離={0:F2}, 次回交換時期={1:F2} ",SouSoukouKyori, SouSoukouKyoriAtNextOilChange);
                    flgDisplayed = true;
                }
                Console.Write("給油しますか？(y/n)=");
                var strFuelWillness = Console.ReadLine();
                if (strFuelWillness == "y") {
                    //ＧＳへ       (残量)
                    (TankZanryo, SouSoukouKyori) = RunBetweenHomeToGs(5, NenPi, TankZanryo);
                    if (GasKetsuCheck(TankZanryo)) break;

                    //満タン       (残量）
                    //必要な供給量
                    TankZanryo = FuelCharge();

                    //オイルチェック
                    if (!OilCheck(SouSoukouKyori, SouSoukouKyoriAtNextOilChange)) {
                        if (!flgDisplayed) Console.WriteLine("オイル交換必要です。");
                        (bool flgCharged, SouSoukouKyoriAtLastOilChange, SouSoukouKyoriAtNextOilChange)
                            = OilCharge(SouSoukouKyori, SouSoukouKyoriAtLastOilChange, OilChangeInterval);
                    }
                }

                //家に戻る      （残量）
                (TankZanryo, SouSoukouKyori) = RunBetweenHomeToGs(5, NenPi, TankZanryo);
                if (GasKetsuCheck(TankZanryo)) break;
            }
        }
        //走る（オーバライド）
        public (double, double?, double) Travel(double inNenPi, double inTankZanryo) {
            (double? tankZanryo, double? soukouKyori) = base.Travel(inNenPi, inTankZanryo);
            if (tankZanryo == null) {
                return (TankZanryo, null, SouSoukouKyori);
            }
            TankZanryo = (double)tankZanryo;
            SouSoukouKyori += (double)soukouKyori;
            return (TankZanryo, soukouKyori, SouSoukouKyori);

        }
        //走る（家ーＧＳ間）
        public (double, double) RunBetweenHomeToGs(double InDist, double inNenPi, double inTankZanryo) {
            TankZanryo = base.RunBetweenHomeToGs(InDist, inNenPi, inTankZanryo);
            SouSoukouKyori += InDist;
            return (TankZanryo, SouSoukouKyori);
        }

        //オイルチェック(サブクラス用）
        public bool OilCheck(double inSouSoukouKyori, double inSouSoukouKyoriAtNextOilChange) {
            return (inSouSoukouKyori < inSouSoukouKyoriAtNextOilChange);
        }

        //オイル交換（サブクラス用）
        public (bool, double, double) OilCharge(double inSouSoukouKyori, double inSouSoukouKyoriAtLastOilChange,double inOilChangeInterval) {
            bool flgCharged = false;
            Console.Write("オイル交換しますか？");
            string strKoukanWillness = Console.ReadLine();
            if (strKoukanWillness == "y" || strKoukanWillness == "Y") {
                SouSoukouKyoriAtLastOilChange = inSouSoukouKyori;
                SouSoukouKyoriAtNextOilChange = inSouSoukouKyoriAtLastOilChange + inOilChangeInterval;
                flgCharged = true;
                Console.WriteLine("オイル交換しました");
            }
            return (flgCharged, SouSoukouKyoriAtLastOilChange, SouSoukouKyoriAtNextOilChange);
        }

        public void Dispose() {
           
        }
    }
}

