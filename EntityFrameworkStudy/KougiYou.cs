using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkStudy;
using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkStudy {
    public class KougiYou {

        private static EntityFrameworkStudyContext _context;
        public KougiYou(EntityFrameworkStudyContext context) {
            _context = context;
        }

        public void KougiYouMethod() {

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


            Environment.Exit(0);


            var result = _context.Education.Where(x => x.ClassCode == "A").OrderBy(x => x.SeitoNo).ToList();

            var d = _context.Education;
            var e = d.Where(x => x.ClassCode == "A" && x.SeitoNo == "01");
            var xx = e.ToList();


            //List<Education> a = _context.Education.Include(c => c.ClassAttr).ToList();

            var b = _context.ClassAttr.Include(x => x.Educations).ToList();

            //var c = _context.ClassAttr.ToList();
            //       var c = _context.Education.Include(x => x.ClassAttr).ToList();

            var dt = _context.Education.ToList();
            dt[0].SeitoNo = "1";

            //dt.Add(xxx);

            //_context.Education.Add(xxxx);
            //_context.Education.Remove();

            //_context.SaveChanges();
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
