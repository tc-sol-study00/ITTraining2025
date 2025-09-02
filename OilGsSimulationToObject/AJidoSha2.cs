using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {
    
    //抽象クラス
    public abstract class AJidoSha2 {

        //abstractを書くと、継承先の実装を強制できる
        public abstract double TankFull { get; set; }
        public abstract double TankLimit { get; set; }
        public abstract double TankZanryo { get; set; }
        public abstract double NenPi { get; set; }

        /*
         * 走行
         */
        //Soukouを継承先で必ず実装させる
        public abstract double Soukou(int argSoukouKyori);

        //以下は、具象メソッドでプログラムを記述できる（一般的なインターフェースとの違い）
        public double RunBetweenHomeToGs(double argMovedDist) {
            double tankZanryo = TankZanryo - (argMovedDist / NenPi);

            TankZanryo = tankZanryo;

            return TankZanryo;
        }

        public double RunBetweenGsToHome(double argMovedDist) {
            return RunBetweenHomeToGs(argMovedDist);
        }

        /*
        * ガス残量チェック
        */

        public bool GsRemainCheck() {
            string displayStatus = "";
            bool checkStatus = TankZanryo >= TankLimit;

            if (checkStatus) {
                displayStatus = "OK";
            }
            else {
                displayStatus = "NG";
            }

            //上記のifたちと同意
            displayStatus = checkStatus ? "OK" : "NG";

            Console.WriteLine("残量チェック {0:F2} 結果={1}", TankZanryo, displayStatus);

            return checkStatus;
        }

        /*
         * ガス欠チェック
         */

        public bool GasKetsuCheck() {
            if (TankZanryo <= 0) {
                Console.WriteLine("ガス欠です");
                return (true);
            }
            else {
                return (false);
            }
        }
    }
}
