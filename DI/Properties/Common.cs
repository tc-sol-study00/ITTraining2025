using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Properties {
    internal class Common {
        internal decimal ConsumptionCulcration(decimal argPriceBeforeTax, decimal argConsumptionTaxPercent) {

            decimal priceWithConsumptionTax;    //税込み価格

            //税込み価格計算
            priceWithConsumptionTax = argPriceBeforeTax * (1 + argConsumptionTaxPercent / 100.0m);
            //税込み価格小数点以下切り捨て
            priceWithConsumptionTax = Math.Floor(priceWithConsumptionTax);  //小数点以下切り捨て

            return priceWithConsumptionTax;
        }
    }
}
