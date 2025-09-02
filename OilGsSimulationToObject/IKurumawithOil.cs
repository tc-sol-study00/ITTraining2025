using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {
    internal interface IKurumawithOil : IDisposable{

        /* 以前のIFはメンバ変数が定義できなかった。今は出来る。 */
        public double SouSoukouKyori { get; set; }
        public double OilChangeInterval { get; set; }
        public double SouSoukouKyoriAtLastOilChange { get; set; }
        public double SouSoukouKyoriAtNextOilChange { get; set; }
        public void GsOilSimulation();
        public (double, double?, double) Travel(double inNenPi, double inTankZanryo);
        public (double, double) RunBetweenHomeToGs(double InDist, double inNenPi, double inTankZanryo);
        public bool OilCheck(double inSouSoukouKyori, double inSouSoukouKyoriAtNextOilChange);
        public (bool, double, double) OilCharge(double inSouSoukouKyori, double inSouSoukouKyoriAtLastOilChange, double inOilChangeInterval);
        public void Dispose() { }
    }
     
    internal abstract class AKurumawithOil :IDisposable{

        public double SouSoukouKyori { get; set; }
        public double OilChangeInterval { get; set; }
        public double SouSoukouKyoriAtLastOilChange { get; set; }
        public double SouSoukouKyoriAtNextOilChange { get; set; }
        public abstract (double, double?, double) Travel(double inNenPi, double inTankZanryo);
        public abstract (double, double) RunBetweenHomeToGs(double InDist, double inNenPi, double inTankZanryo);
        public abstract bool OilCheck(double inSouSoukouKyori, double inSouSoukouKyoriAtNextOilChange);
        public abstract (bool, double, double) OilCharge(double inSouSoukouKyori, double inSouSoukouKyoriAtLastOilChange, double inOilChangeInterval);

        public void Dispose() { }
    }
 

}
