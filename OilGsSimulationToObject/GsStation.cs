using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilGsSimulationToObject {
    internal class GsStation {
        /*
         * 満タン
         */
        public double FuelCharge(IJidoSha argJidoSha) {
            var TankZanryoBeforeCharge = argJidoSha.TankZanryo;

            var requiredFuel = argJidoSha.TankFull - argJidoSha.TankZanryo;
            argJidoSha.TankZanryo += requiredFuel;
            Console.WriteLine("現在の残量={0:F2} {1:F2} 給油して {2:F2}リットル 満タンにしました", TankZanryoBeforeCharge, requiredFuel, argJidoSha.TankZanryo);

            return argJidoSha.TankZanryo;
        }

        /*
         * オイル交換
         */
        public bool OilCharge(JidoShawithOil argJidoShawithOil) {
            bool flgCharged = false;

            Console.Write("オイル交換しますか？");
            string? strKoukanWillness = Console.ReadLine();
            if (strKoukanWillness == "y" || strKoukanWillness == "Y") {
                argJidoShawithOil.SouSoukouKyoriAtLastOilChange = argJidoShawithOil.SouSoukouKyori;
                argJidoShawithOil.SouSoukouKyoriAtNextOilChange = argJidoShawithOil.SouSoukouKyoriAtLastOilChange + argJidoShawithOil.OilChangeInterval;
                flgCharged = true;
                Console.WriteLine("オイル交換しました");
            }

            return flgCharged;
        }
    }
}
