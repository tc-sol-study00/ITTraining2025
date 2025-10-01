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

            var button1 = new Button(Method1);
            var button2 = new Button(Method2);

            Dictionary<int, Button> dic 
                = new Dictionary<int, Button>() {{ 1, button1 }, { 2, button2 }};

            int intInput;
            string? strInput;
            while (true) {
                do {
                    Console.Write("Input Menu No=");
                    strInput = Console.ReadLine();
                } while (!int.TryParse(strInput, out intInput));

                if (dic.ContainsKey(intInput)) {
                    dic[intInput].Click();
                }
            }
        }

        public void Method1() {
            Console.WriteLine("Method1実行");
        }
        public void Method2() {
            Console.WriteLine("Method2実行");
        }

    }
}
