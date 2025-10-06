using Convenience.Data;
using Convenience.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStudy {
    public class Chumon {
        internal IList<ChumonJissekiMeisai> ChumonJissekiMeisais { get; set; }

        private readonly ConvenienceContext _context;
        public Chumon(ConvenienceContext context) {
            _context = context;
        }

        public IList<ChumonJissekiMeisai> Kensaku() {
            _context.ChangeTracker.Clear();
            return ChumonJissekiMeisais=
                _context.ChumonJissekiMeisai
                    .OrderBy(c => c.ChumonId)
                    .ThenBy(c => c.ShiireSakiId)
                    .ThenBy(c => c.ShiirePrdId)
                    .ToList();
        }
        public void Reset() {
            _context.ChangeTracker.Clear();
            ChumonJissekiMeisais = new List<ChumonJissekiMeisai>();
        }
        public void Save() {
            _context.SaveChanges();
        }

    }
}
