using Delegate.Properties;

namespace Delegate.Services {
    internal class Service_Layer3 {

        private readonly Common _common;

        //レイヤー3はComonを使う
        public Service_Layer3(Common common, Common2 common2) {
            _common = common;   //移譲でもらった
        }
        public void Layer3_Method(decimal argNetPrice) {
            //もらったものを活用
            decimal priceWithTax=_common.ConsumptionCulcration(argNetPrice, 8);
        }

    }
}
