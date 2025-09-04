using EntityFrameworkStudy;
using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace EntityFrameworkStudy {
    public class KougiYou {

        private static EntityFrameworkStudyContext _context;
        public KougiYou(EntityFrameworkStudyContext context) {
            _context = context;
        }

        public void KougiYouMethod() {
            {
                var educations = _context.Education;

                //鉄板
                var educations2 = _context.Education.Where(w => w.ClassCode == "A" && w.SeitoNo == "01")
                    .OrderBy(o => o.ClassCode).ThenBy(o => o.SeitoNo).Select(x => new { x.ClassCode, x.SeitoNo });

                //結合(Flat）
                var education3 = _context.Education
                    .Join(_context.ClassAttr,
                        ed => ed.ClassCode,
                        cl => cl.ClassCode,
                        (ed, cl) => new {
                            Education = ed,
                            ClassAttr = cl
                        });

                //結合(Nest）
                var education4 = _context.Education.Include(x => x.ClassAttr);
                var education5 = _context.ClassAttr.Include(x => x.Educations);

                List<Education> educations6 = _context.Education.ToList();

                //遅延実行
                var educations7 = _context.Education;
                var educations8 = educations7.OrderBy(x => x.ClassCode);

                int xxxx=1;
                if (xxxx == 1) {
                    educations8 = educations8.ThenBy(x => x.SeitoNo);
                }
                var educations10 = educations8.ToList();

                //悪い例
                List<Education> educations11 = _context.Education.Where(x => x.ClassCode == "A" && x.SeitoNo == "01").ToList();
                if (educations11.Count == 0) {

                }
                Console.WriteLine(educations11[0].SeitoNo);

                //正しい例
                Education? education12 = _context.Education.Where(x => x.ClassCode == "A" && x.SeitoNo == "01").FirstOrDefault();

                //グループ化

                var educationxx = _context.Education
                    .GroupBy(x => new { x.ClassCode, x.SeitoNo })
                    .Select(g => new {
                        ClassCode = g.Key.ClassCode,
                        SeitoNo = g.Key.SeitoNo,
                        TotalScore = g.Sum(x => x.KokugoScore) // 集計したいプロパティを指定
                    })
                    .ToList();

                //外部結合

                var educationyy = _context.Education
                    .GroupJoin(
                        _context.ClassAttr,
                        ed => ed.ClassCode,       // 外部キー
                        cl => cl.ClassCode,       // 主キー
                        (ed, cls) => new { ed, cls } // cls はコレクションになる
                    )
                    .SelectMany(
                        x => x.cls.DefaultIfEmpty(), // cls が空なら null
                        (x, cl) => new {
                            Education = x.ed,
                            ClassAttr = cl
                        }
                    )
                    .ToList();
            }


            {
                //IEnumerable -> ICollect -> IList -> List

                //IEnumerable
                IEnumerable<Education> educations = _context.Education;

                foreach (Education aEducation in educations) {
                    Console.WriteLine("ClassCode={0}:SeitoNo={1}", aEducation.ClassCode, aEducation.SeitoNo);
                }

                //ICollection

                ICollection<Education> educations1 = new Collection<Education>(_context.Education.ToList());

                Education education = new Education { ClassCode = "A", SeitoNo = "10" };
                educations1.Add(education);
                educations1.Remove(education);

                //Ilist(加えてインデクサが使える

                IList<Education> educations2 = _context.Education.ToList();

                for (int i = 0; i < educations2.Count; i++) {
                    Console.WriteLine("ClassCode={0}:SeitoNo={1}", educations2[i].ClassCode, educations2[i].SeitoNo);
                }


                //ListはIlistを実装している（
                List<Education> educations3 = _context.Education.ToList();

                for (int i = 0; i < educations3.Count; i++) {
                    Console.WriteLine("ClassCode={0}:SeitoNo={1}", educations2[i].ClassCode, educations2[i].SeitoNo);
                }

                //引数にList型データをいれているが、メソッド側はIEnumerabled
                //List型はIList型を実装しており、IList側はIEnimerable型を継承しているので、引数に渡せる（反変性）

                if (CheckData(educations3)??false) {
                    Console.WriteLine("Data OK");
                }
                else {
                    Console.WriteLine("Data NG");
                }

                //以下は共変性
                List<Education> rdata=_context.Education.ToList();
                IEnumerable<Education> datax = rdata;
            }
        }

        public bool? CheckData(IEnumerable<Education> argEducations) {

            if(argEducations == null) return null; 

            bool result = true;
            foreach ( Education aEducation in argEducations) {
                 result = result && aEducation.SuugakuScore >= 0 && aEducation.RikaScore >= 0 && aEducation.KokugoScore >= 0;
            }

            return result;

        }


        public void AddData() {
            List<ClassAttr> classAttrs = new List<ClassAttr> { new ClassAttr { ClassCode = "A", Tannin = "Aさん" } };

            List<Education> educations = new List<Education> {
                new Education { ClassCode = "A", SeitoNo="01", KokugoScore=10,SuugakuScore=11,RikaScore=12 },
                new Education { ClassCode = "A", SeitoNo="02", KokugoScore=20,SuugakuScore=21,RikaScore=22 },
                new Education { ClassCode = "A", SeitoNo="03", KokugoScore=30,SuugakuScore=31,RikaScore=32 },
            };

            foreach (var item in classAttrs) {
                var exist = _context.ClassAttr.Find(item.ClassCode);
                if (exist == null) {
                    _context.ClassAttr.Add(item);
                }
            }

            foreach (var item in educations) {
                var exist = _context.Education.Find(item.ClassCode, item.SeitoNo);
                if (exist == null) {
                    _context.Education.Add(item);
                }
            }
            _context.SaveChanges();
        }

        //genericとdefaultについて
        public class NullChosa<T> {

            public void Method1(T inData) {

                //intのときのdefaultは0
                //int?のときのdefaultはnull
                if (inData.Equals(default(T))) {
                    //処理
                }
            }
        }

        public class MainPoi {
            public void MainPoiMethod() {
                new NullChosa<int>().Method1(0);
                new NullChosa<int?>().Method1(null);
            }
        }

        //省略形
        public class ChottoDake {

            public bool Method2(int? dd) {
                Nullable<int> x; //これは int? xと同じ
                var a = dd ?? 0;      //ddがnullなら0、どうでないならddの値適用
                var b = dd > 0 ? 1 : 2;   //ddが0より多い場合、1、そうでなければ2
                bool c = b > 1;       //b>1の比較結果をtrueかfalseでセット
                return (b > 1);     //b>1の比較結果をtrueかfalseで返却
            }

        }

        void Hidoki() {
            List<int> int1s = new List<int> { 1, 2, 3, 4, 5 };
            List<int> int2s = new List<int> { 1, 2, 3, 4, 5 };

            for (int i = 0; i < int1s.Count; i++) {
                for (int j = 0; j < int2s.Count; j++) {
                    Task<int> r = Add(int1s[i], int2s[j]);
                }
            }
        }

        Task<int> Add(int a, int b) {
            return Task.FromResult(a + b);
        }

    }
}
