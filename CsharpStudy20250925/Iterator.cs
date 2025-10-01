using Convenience.Data;
using Convenience.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpStudy20250925 {
    internal class Iterator : IEnumerable<int> {

        //イテレータ
        public IEnumerator<int> GetEnumerator() {

            //通常のreturn→メソッド終了させ値を一つ返す
            //yield return→メソッドを終了せず、一時的に値を呼び出し元に返す。
            ///一度に全部のデータを返すのではなく、必要になったタイミングで1つずつ返す仕組み
            ///
            for (int c = 1; c <= 3; c++) {
                yield return c;
            }

            //List<ChumonJisseki> dts = _context.ChumonJisseki.ToList();

            //foreach ( var item in dst) {
            //    Console.WriteLine(item);
            //}
        }

        //イテレータ実装の際に必要な記述
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    internal class ChumonData {
        internal string ChumonId { get; set; }

        internal string ShiirePrdId { get; set; }

        internal string ShiirePrdName { get; set; }

        internal decimal ChumonSu { get; set; }

    }

    internal class ChumonDatas : IEnumerable<ChumonData> {

        private readonly ConvenienceContext _context;

        private IEnumerable<ChumonData> _data;
        public ChumonDatas(ConvenienceContext context) {
            _context = context;
            _data = _context.ChumonJisseki
                .Include(cj => cj.ChumonJissekiMeisais)
                    .ThenInclude(cm => cm.ShiireMaster)
                .SelectMany(cj => cj.ChumonJissekiMeisais.DefaultIfEmpty(),
                            (cj, cm) => new { ChumonJisseki = cj, Meisai = cm })
                .Select(x => new ChumonData { ChumonId = x.ChumonJisseki.ChumonId, ShiirePrdId = x.Meisai.ShiirePrdId, ShiirePrdName = x.Meisai.ShiireMaster.ShiirePrdName, ChumonSu = x.Meisai.ChumonSu })
            ;
        }

        public IEnumerator<ChumonData> GetEnumerator() {
            foreach (var aChumonData in _data) {
                yield return aChumonData;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}