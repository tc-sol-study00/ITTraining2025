namespace OilGsSimulationToObject {
    enum Proc {
        DriverOperation_Normal,
        DriverOperation_Interface
    }

    class Programs {
        static void Main() {
            Proc flg = Proc.DriverOperation_Normal;

            switch (flg) {
                case Proc.DriverOperation_Normal:
                    new DriverOperation_Normal();
                    break;
                case Proc.DriverOperation_Interface:
                    new DriverOperation_Interface();
                    break;
                default:
                    break;
            }
        }
    }
}
