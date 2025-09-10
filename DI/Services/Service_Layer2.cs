using DI.Properties;

namespace DI.Services {
    internal class Service_Layer2 {

        private readonly Service_Layer3 _service_Layer3;
        public Service_Layer2(Service_Layer3 _service_Layer3) {
            _service_Layer3 = _service_Layer3;
        }
    }
}
