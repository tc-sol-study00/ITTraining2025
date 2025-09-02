namespace OilGsSimulationToObject {

    public class JidoSha2 : AJidoSha2,IJidoSha2 {
        //メンバー
        public string CarName { get; set; }
        public override double NenPi { get; set; }
        public override double TankFull { get; set; }
        public override double TankLimit { get; set; }
        public override double TankZanryo { get; set; }
        //コンストラクタ 
        public JidoSha2(string CarName, double NenPi, double TankFull, double LimitPct) {
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

        public override double Soukou(int argSoukouKyori) {

            double retTankZanryo = TankZanryo - (argSoukouKyori / NenPi);

            Console.WriteLine("走行距離= {0:D5}", argSoukouKyori);

            TankZanryo = retTankZanryo;

            return TankZanryo;
        }

    }
}
