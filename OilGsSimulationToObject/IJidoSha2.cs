using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {
    public interface IJidoSha2 {
        //メンバー
        public string CarName { get; set; }
        public double NenPi { get; set; }
        public double TankFull { get; set; }
        public double TankLimit { get; set; }
        public double TankZanryo { get; set; }

        //メソッド

        /*
 * 走行
 */
        public double Soukou(int argSoukouKyori);

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

    public interface  IJidoSha3 {
        public double Soukou(int argSoukouKyori);
    }
}
