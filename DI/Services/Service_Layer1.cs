using DI.Properties;

namespace DI.Services {
    internal class Service_Layer1 {

        private readonly Service_Layer2 _service_Layer2;

        public Service_Layer1(Service_Layer2 service_Layer2) {
            _service_Layer2 = service_Layer2;
        }
        public void Layer1_Method() {
            //Layer1の処理
            //commonは使わない
        }
    }
}