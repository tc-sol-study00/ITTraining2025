using Convenience.Data;
using Convenience.Models.DataModels;

namespace EventStudy2 {
    public class Chumon {
        internal IEnumerable<ChumonJissekiMeisai> ChumonJissekiMeisais { get; set; }

        IList<ChumonJissekiMeisai> ChumonJissekiMeisaisList() => ChumonJissekiMeisais.ToList();

        internal int ShoriCount { get; set; } = 0;

        private readonly ConvenienceContext _context;
        public Chumon(ConvenienceContext context) {
            _context = context;
        }

        public IEnumerable<ChumonJissekiMeisai> Kensaku() {

            _context.ChangeTracker.Clear();

            return ChumonJissekiMeisais =
                _context.ChumonJissekiMeisai
                    .OrderBy(c => c.ChumonId)
                    .ThenBy(c => c.ShiireSakiId)
                    .ThenBy(c => c.ShiirePrdId)
                    ;
        }
        public void Reset() {
            _context.ChangeTracker.Clear();
            ChumonJissekiMeisais = new List<ChumonJissekiMeisai>();
        }
        public void Save() {
            _context.SaveChanges();
        }

        public async Task ChumonSummary() {
            foreach( var item in Enumerable.Range(1, 1000)){
                await Task.Delay(50);
                ShoriCount = item;
            }
        }
        public void ChumonSummarySync() {
            foreach (var item in Enumerable.Range(1, 1000)) {
                Thread.Sleep(50);
                ShoriCount = item;
                Application.DoEvents();
            }
        }
    }
}
