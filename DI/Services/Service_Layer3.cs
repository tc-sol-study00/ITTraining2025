using DI.Properties;

namespace DI.Services {
    internal class Service_Layer3 {

        private readonly Common _common;
        public Service_Layer3(Common common) {
            _common = common;
        }
        public void Layer3_Method(decimal argNetPrice) {
            decimal priceWithTax = _common.ConsumptionCulcration(argNetPrice, 8);
        }
    }
}
