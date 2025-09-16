using EntityFrameworkStudy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudy {
    internal class LinqJoin {

        private static EntityFrameworkStudyContext _context;

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
                .ToList();

            Console.WriteLine(new string('-', 20));
            query1.ForEach(x =>
                Console.WriteLine($"{x.cls.ClassCode},{x.cls.Tannin},{x.edu.SeitoNo},{x.edu.KokugoScore}"));


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
                    .ToList();

            Console.WriteLine(new string('-', 20));
            query2.ForEach(x =>
                Console.WriteLine($"{x.cls.ClassCode},{x.cls.Tannin},{x.edu?.SeitoNo ?? string.Empty},{x.edu?.KokugoScore ?? 0}"));

            //GroupBy
            var query3 = _context.Education
                .GroupBy(x => x.ClassCode)
                .Select(g => new {
                    g.Key,
                    TotalKokugoScore = g.Sum(e => e.KokugoScore)  // 合計
                });

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
                .Select(g => new { g.Key.ClassCode, g.Key.Tannin, TTL = g.Sum(e => e.edu.KokugoScore) })
                .ToList();

            Console.WriteLine(new string('-', 20));
            query4.ForEach(x =>
                Console.WriteLine($"{x.ClassCode},{x.Tannin},{x.TTL}"));

        }
    }
}
