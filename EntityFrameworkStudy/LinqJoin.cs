using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudy {
    internal class LinqJoin {

        private static EntityFrameworkStudyContext _context;
        private static void SQLDisplay<T>(IQueryable<T> query) where T : class =>
            Console.WriteLine($"{new string('-', 20) + "SQL↓\n"}{query.ToQueryString()}");

        public LinqJoin(EntityFrameworkStudyContext context) {
            _context = context;
        }
        public void LinqJoinTest() {

            //内部結合
            var query1 = _context.ClassAttr //結合元
                .Join(_context.Education,   //結合先(Joinの第1引数)     
                cls => cls.ClassCode,           //結合元の結合キー(Joinの第2引数)
                edu => edu.ClassCode,           //結合元の結合キー(Joinの第3引数)
                (cls, edu) => new { cls, edu }) //cは、結合元のベクトル、eは結合先のベクトル（フラット）(Joinの第4引数)
                ;

            Console.WriteLine(new string('-', 20));
            query1.ToList().ForEach(x =>
                Console.WriteLine($"{x.cls.ClassCode},{x.cls.Tannin},{x.edu.SeitoNo},{x.edu.KokugoScore}"));

            SQLDisplay(query1);


            //外部結合
            var query2 = _context.ClassAttr  //結合元
                    .GroupJoin(
                        _context.Education,     //結合先(GroupJoinの第1引数) 
                        c => c.ClassCode,       //結合元の結合キー(GroupJoinの第2引数)
                        e => e.ClassCode,       //結合先の結合キー(GroupJoinの第3引数)
                        (c, edus) => new { c, edus }    //cに結合元のスカラー、edusに結合先のベクトルが入る（ネスト)(GroupJoinの第4引数)
                    )
                    .SelectMany(                //xには上記のcとedusが入る
                        x => x.edus.DefaultIfEmpty(),   //これがeduになる
                        (x, edu) => new { cls = x.c, edu = edu }    //第1引数にSelectManyのデータ入れ元、eduが、nullを含めたデータ
                    )
                    ;

            Console.WriteLine(new string('-', 20));
            query2.ToList().ForEach(x =>
                Console.WriteLine($"{x.cls.ClassCode},{x.cls.Tannin},{x.edu?.SeitoNo ?? string.Empty},{x.edu?.KokugoScore ?? 0}"));

            SQLDisplay(query2);

            //GroupBy
            var query3 = _context.Education
                .GroupBy(x => new { x.ClassCode, x.SeitoNo })
                .Select(g => new {
                    g.Key.ClassCode,
                    g.Key.SeitoNo,
                    TotalKokugoScore = g.Sum(e => e.KokugoScore)  // 合計
                });

            query3.ToList().ForEach(x => Console.WriteLine($"{x.ClassCode}:{x.SeitoNo}:{x.TotalKokugoScore}"));
            SQLDisplay(query3);

            //外部結合＋GroupBy
            var query4 = _context.ClassAttr  //結合元
                .GroupJoin(
                   _context.Education,     //結合先
                   c => c.ClassCode,       //結合元の結合キー
                   e => e.ClassCode,       //結合先の結合キー
                   (c, edus) => new { c, edus }    //cに結合元のスカラー、edusに結合先のベクトルが入る
                )
                .SelectMany(
                x => x.edus.DefaultIfEmpty(),
                (x, edu) => new { cls = x.c, edu }
                )
                .GroupBy(x => new { x.cls.ClassCode, x.cls.Tannin })
                .Select(g => new { g.Key.ClassCode, g.Key.Tannin, TTL = g.Sum(e => e.edu.KokugoScore) });

            Console.WriteLine(new string('-', 20));
            query4.ToList().ForEach(x =>
                Console.WriteLine($"{x.ClassCode},{x.Tannin},{x.TTL}"));

            SQLDisplay(query4);


            //あるかないか系

            if (_context.ClassAttr.Any(x => x.ClassCode == "C")) {
                Console.WriteLine("ある");
            }
            else {
                Console.WriteLine("ない");
            }

            //順位付け

            var educationRank = _context.Education
                .OrderByDescending(o => o.SuugakuScore + o.RikaScore + o.KokugoScore)
                .AsEnumerable()
                .Select((x, index) => new {
                    x.ClassCode,
                    x.SeitoNo,
                    TotalScore = x.SuugakuScore + x.RikaScore + x.KokugoScore,
                    Rank = index + 1
                });

            educationRank.ToList().ForEach(x => Console.WriteLine("{0}:{1}:{2}:{3}", x.ClassCode, x.SeitoNo, x.TotalScore, x.Rank));

            //GroupByはグループキーを元に、関係データをネスト化する
            var orderdEducations = _context.Education
                .GroupBy(x => x.ClassCode)
                .Select(g => new {
                    Key = g.Key,        // クラスコードと生徒番号
                    Educations = g.ToList() // その生徒の成績リスト
                })
                .ToList();

            //それを集計する
            var orderdEducationsSum = _context.Education
                .GroupBy(x => x.ClassCode)
                .Select(g => new {
                    ClassCode = g.Key,        // クラスコードと生徒番号
                    SumAllScore = g.Sum(d => d.SuugakuScore + d.KokugoScore + d.RikaScore)
                })
                .ToList();

            //Firstはスカラーでデータを返すが、データが無かったらAbendする
            if (_context.Education.Any(x => x.ClassCode == "B")) {
                var educationFirst = _context.Education.Where(x => x.ClassCode == "B").First();
            }
            //Singleはデータがない場合と複数件データが変える場合にAbendする
            //var educationSingle = _context.Education.Where(x => x.ClassCode == "B").Single();
            //var educationSingles = _context.Education.Where(x => x.ClassCode == "A").Single();

            List<string> stringdata1 = new List<string>() { "1", "2", "3", "4", "5" };
            List<string> stringdata2 = new List<string>() { "2", "3","3", "5","5","5" };



        }
    }
}
