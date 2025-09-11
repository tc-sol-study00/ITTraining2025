using Delegate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Services {
    internal class Service_Layer2 {

        private readonly Service_Layer3 _service_Layer3;
        public Service_Layer2(Common common, Common2 common2) {
            _service_Layer3 = new Service_Layer3(common,common2);  //Comonを使わないが、以下のレイヤーのためにバトンタッチ必要
        }
    }
}
