namespace OilGsSimulationToObject {

    public class JidoSha : IJidoSha {
       
        //メンバー
        public string CarName { get; set; }
        public double NenPi { get; set; }
        public double TankFull { get; set; }
        public double TankLimit { get; set; }
        public double TankZanryo { get; set; }

        //コンストラクタ 
        public JidoSha(string CarName, double NenPi, double TankFull, double LimitPct) {
            this.CarName = CarName;
            this.NenPi = NenPi;
            this.TankFull = TankFull;
            this.TankLimit = TankFull * LimitPct;
            this.TankZanryo = TankFull;
        }

        //メソッド

        /*
         * 走行
         */

        public virtual double Soukou(int argSoukouKyori) {

            double retTankZanryo = TankZanryo - (argSoukouKyori / NenPi);

            Console.WriteLine("走行距離= {0:D5}", argSoukouKyori);

            TankZanryo = retTankZanryo;

            return TankZanryo;
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
         * 家ーＧＳ間
         */
        public double RunBetweenHomeToGs(double argMovedDist) {
            double tankZanryo = TankZanryo - (argMovedDist / NenPi);

            TankZanryo = tankZanryo;

            return TankZanryo;
        }

        public double RunBetweenGsToHome(double argMovedDist) {
            return RunBetweenHomeToGs(argMovedDist);
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
