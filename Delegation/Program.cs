using Delegate.Properties;
using Delegate.Services;
using System.Net.Http.Headers;

namespace Delegate {
    internal class Program {

        private readonly Common _common;    //共通部分なのでmainで生成
        private readonly Common2 _common2;    //共通部分なのでmainで生成
        private readonly Service_Layer1 _service_Layer1;

        // Program のコンストラクターで初期化
        public Program() {
            _common = new Common(); //共通部分
            _common2 = new Common2(); //共通部分
            _service_Layer1 = new Service_Layer1(_common,_common2);  //サービス第一レイヤー
        }

        static void Main(string[] args) {
            var program = new Program();
            program.Run();
        }

        // Main の処理を分離
        private void Run() {
            _service_Layer1.Layer1_Method();    //第一レイヤーのメソッド実行
        }
    }
}
