using Convenience.Data;
using Convenience.Models.DataModels;

namespace CsharpStudy20250925 {
    internal class DictionalyStudy {

        private enum Enum_ProcNo {
            Dictionally,
            DictionallySeveral,
            Queue,
            Stack,
            Object,
            Dynamic,
            Generic,
            ToLookUp,
            TakeSkip,
            Zip,
            Split,
            EFCore
        }
        private static readonly List<Enum_ProcNo> ProcNo
            = new List<Enum_ProcNo>
                {
                  Enum_ProcNo.Dictionally,
                  Enum_ProcNo.DictionallySeveral,
                  Enum_ProcNo.Queue,
                  Enum_ProcNo.Stack,
                  Enum_ProcNo.Object,
                  Enum_ProcNo.Dynamic,
                  Enum_ProcNo.Generic,
                  Enum_ProcNo.ToLookUp,
                  Enum_ProcNo.TakeSkip,
                  Enum_ProcNo.Zip,
                  Enum_ProcNo.Split,
                  Enum_ProcNo.EFCore,
                };

        private readonly ConvenienceContext _context;

        public DictionalyStudy(ConvenienceContext context) {
            _context = context;
        }
        public void Run() {

            foreach (var proc in ProcNo) {

                switch (proc) {

                    case Enum_ProcNo.Dictionally:
                        //Dictionaly 使い方

                        Dictionary<string, int> dic = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 } };

                        dic["C"] = 3;

                        int a;
                        if (dic.ContainsKey("D")) {
                            a = dic["D"];
                        }

                        if (dic.TryGetValue("D", out a)) {
                            //aの値が参照できる
                        }
                        else {
                            //エラー
                        }

                        //上記の短い書き方

                        a = dic.TryGetValue("D", out var val) ? val : 0;
                        break;

                    case Enum_ProcNo.DictionallySeveral:

                        //キー項目が複数
                        Dictionary<(string, string), int> list = new Dictionary<(string, string), int>()
                        {{ ("A", "B"), 1 },{ ("C", "D"), 2 }};

                        var dt = list[("A", "B")];

                        break;

                    case Enum_ProcNo.Queue:
                        //Queue
                        Queue<string> queue = new Queue<string>();
                        queue.Enqueue("A");
                        queue.Enqueue("B");
                        queue.Enqueue("C");

                        while (queue.Count > 0) {
                            Console.WriteLine(queue.Dequeue());
                        }

                        break;

                    case Enum_ProcNo.Stack:

                        //Stack
                        Stack<string> stacks = new Stack<string>();
                        stacks.Push("A");
                        stacks.Push("B");
                        stacks.Push("C");

                        while (stacks.Count > 0) {
                            Console.WriteLine(stacks.Pop());
                        }

                        break;

                    case Enum_ProcNo.Object:

                        //object型(intもクラスも、object型を継承している＝なんでも入る

                        List<object> objList = new List<object>();
                        objList.Add(1);
                        objList.Add("Hello");
                        objList.Add(DateTime.Now);

                        foreach (var key in objList) {
                            DisplayObj(key);
                        }

                        break;

                    case Enum_ProcNo.Dynamic:

                        //dynamic型（コンパイル時はNoチェック、実行時にチェックされる

                        dynamic j1 = "0";    //OK
                        int l = j1.Length;

                        dynamic j2 = 0;    //NG
                        //l = j2.Length; //int型に、Lengthがないから

                        var k = 0;
                        //l = k.Length; //コンパイル時、すでにintになるとわかっているので、エラーになる

                        break;

                    case Enum_ProcNo.Generic:

                        //ジェネリック
                        ChumonJisseki chumonJisseki = new ChumonJisseki() { ChumonId = "20250925-001", ChumonDate = DateOnly.FromDateTime(DateTime.Now) };
                        GenericUsing(chumonJisseki, nameof(chumonJisseki.ChumonId));
                        break;
                    case Enum_ProcNo.ToLookUp:

                        //ToLookup
                        List<dynamic> datas= new List<dynamic>() 
                            { new { Name = "aaaa", Score = 10 }, new { Name = "bbbb", Score = 20 } };
                        
                        var result=datas.ToLookup(x => x.Name);

                        var score=result["aaaa"].Select(x => x.Score);
                        break;

                    case Enum_ProcNo.TakeSkip:

                        //Skip,Take
                        List<int> data2 = new List<int>() { 1,2,3,4,5,6,7 };
                        var data3=data2.Skip(2).Take(2);
                        data3.ToList().ForEach( x => Console.WriteLine(x));

                        //TakeLast
                        data3=data2.TakeLast(2);
                        data3.ToList().ForEach(x => Console.WriteLine(x));

                        //SkipLast
                        data3 = data2.SkipLast(2).TakeLast(1);
                        data3.ToList().ForEach(x => Console.WriteLine(x));

                        break;

                    case Enum_ProcNo.Zip:

                        //Zip
                        List<int> a1 = new List<int> { 1, 2, 3, 4, 5 };
                        List<int> a2 = new List<int> { 11, 21, 31, 41, 51 };
                        List<string> a3 = new List<string> { "A1", "A2", "A3", "A4", "A5" };

                        IEnumerable<(int,int,string)> r=a1.Zip(a2, a3);
                        
                        r.ToList().ForEach(x => Console.WriteLine($"{x.Item1},{x.Item2},{x.Item3}"));
                        break;

                    case Enum_ProcNo.Split:

                        //Split
                        string strData = "1,2,3,4,5,6,7,8";
                        string?[] splitDatas=strData.Split(',');
                        var pickupNumber=splitDatas[2];
                        
                        break;

                    case Enum_ProcNo.EFCore:

                        
                        IEnumerable<ChumonJisseki> chumonJissekis = _context.ChumonJisseki;

                        //ToLookUpは遅延実行ではない
                        var ChumonDic = chumonJissekis.ToLookup(d => (d.ShiireSakiId, d.ChumonId)); //ここでDB取り込み
                        ChumonJisseki? chumonData=ChumonDic[("A000000001","20250917-001")].FirstOrDefault();

                        break;
                    default:
                        break;
                }
            }
        }


        private void DisplayObj(object obj) {
            Console.WriteLine($"{obj.GetType().Name}:{obj}");
        }

        private void GenericUsing<T>(T argData, string argPropertyName) where T : class {

            var gotData = typeof(T).GetProperty(argPropertyName).GetValue(argData);

            Console.WriteLine($"{gotData}");
        }
    }
}
