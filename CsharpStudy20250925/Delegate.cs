using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpStudy20250925 {

    internal class Button {
        public event Action? ClickEvents;   //イベント発動時のメソッドを入れる
        public Button(Action action) {
            ClickEvents += action;
        }
        public void Click() {
            ClickEvents?.Invoke();  //クリックイベント発動
        }

        public void ClearEvent() {
            ClickEvents = null;  //クリックイベント発動
        }
    }
    internal class Delegate {

        internal void Run() {

            //Action/Funcの使い方

            Action<string> cwl = (s1) => Console.WriteLine(s1);
            Func<int, int, string> fc = (x1, x2) => (x1 + x2).ToString();

            cwl("aaa");
            string res = fc(1, 2);

            var kensakuButton = new Button(Kensaku);
            var torokuBottun = new Button(Toroku);
            var sakujoButton = new Button(Sakujo);

            Dictionary<int, Button> dic
                = new Dictionary<int, Button>() { { 1, kensakuButton }, { 2, torokuBottun }, { 3, sakujoButton } };

            int intInput;
            string? strInput;
            while (true) {
                do {
                    Console.Write("Input Menu No=");
                    strInput = Console.ReadLine();
                } while (!int.TryParse(strInput, out intInput));

                if (dic.ContainsKey(intInput)) {
                    //ハンドラ処理なし・クリックイベントを起こすだけ
                    dic[intInput].Click();
                }
            }
        }

        public void Kensaku() {
            Console.WriteLine("Kensaku実行");
        }
        public void Toroku() {
            Console.WriteLine("Toroku実行");
        }

        public void Sakujo() {
            Console.WriteLine("Sakujo実行");
        }

        //Event＋deligateを使わない場合
        internal class OldMethod {

            internal class OldButton {
                internal int _MenuNo { get; set; }
                internal OldButton(int MenuNo) {
                    _MenuNo = MenuNo;
                }
            }

            internal void Run() {

                var kensakuButton = new OldButton(1);
                var torokuBottun = new OldButton(2);

                Dictionary<int, OldButton> dic
                = new Dictionary<int, OldButton>() { { 1, kensakuButton }, { 2, torokuBottun } };


                int intInput;
                string? strInput;
                while (true) {
                    do {
                        Console.Write("Input Menu No=");
                        strInput = Console.ReadLine();
                    } while (!int.TryParse(strInput, out intInput));

                    if (dic.ContainsKey(intInput)) {
                        //ハンドラ処理
                        switch (dic[intInput]._MenuNo) {
                            case 1:
                                Kensaku();
                                break;
                            case 2:
                                Toroku();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            public void Kensaku() {
                Console.WriteLine("Kensaku実行");
            }
            public void Toroku() {
                Console.WriteLine("Touroku実行");
            }
        }
    }
}
