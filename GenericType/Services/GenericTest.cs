using GenericType.Interfaces;
using System.ComponentModel.Design;

namespace GenericType.Services {
    internal class GenericTest {
        internal T ConsumptionCulcration<T>(T argPriceBeforeTax, T argConsumptionTaxPercent) {
            // decimalに変換（string, int, double, decimal など対応）
            decimal priceBeforeTax = Convert.ToDecimal(argPriceBeforeTax);
            decimal consumptionTaxPercent = Convert.ToDecimal(argConsumptionTaxPercent);

            // 税込み価格計算
            decimal priceWithConsumptionTax = priceBeforeTax * (1 + consumptionTaxPercent / 100m);

            // 小数点以下切り捨て
            priceWithConsumptionTax = Math.Floor(priceWithConsumptionTax);

            // 戻り値を元の型に変換

            return (T)Convert.ChangeType(priceWithConsumptionTax, typeof(T));
        }

        //各クラス（Iselectインターフェース実装のクラス）を共通して処理する
        public List<ISelect> ListGenerator<T>(IList<T> ListObject ) where T : class,ISelect  {

            return ListObject.OrderBy(x => x.ListItem).Cast<ISelect>().ToList();

        }


    }




}
