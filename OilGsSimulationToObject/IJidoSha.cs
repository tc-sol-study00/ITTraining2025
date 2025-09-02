using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {

    /// <summary>
    /// 自動車・オイル管理月自動車共通インターフェース
    /// 外部契約書
    /// </summary>
    internal interface IJidoSha {

        public string CarName { get; set; }
        public double TankFull { get; set; }
        public double TankLimit { get; set; }
        public double TankZanryo { get; set; }
        public double Soukou(int argSoukouKyori);
        public double RunBetweenHomeToGs(double argMovedDist);

        public double RunBetweenGsToHome(double argDist);

        public bool GsRemainCheck();

        public bool GasKetsuCheck();
    }
}
