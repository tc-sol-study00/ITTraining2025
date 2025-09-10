using Delegate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Services {
    internal class Service_Layer1 { //サービス第一レイヤー

        private readonly Service_Layer2 _service_Layer2;

        public Service_Layer1(Common common) {  //Comonを使わないが、以下のレイヤーのためにバトンタッチ必要
            _service_Layer2 = new Service_Layer2(common);
        }
        public void Layer1_Method() {
           //Layer1の処理
           //commonは使わない
        }
    }
}
