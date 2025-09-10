using GenericType.DTOs;
using GenericType.Interfaces;
using GenericType.Services;

namespace GenericType {
    internal class Program {

        private static readonly GenericTest _genericTest = new GenericTest();
        static void Main(string[] args) {
            
            /* タイプ違いの処理サンプル */
            
            string srtResult=_genericTest.ConsumptionCulcration<string>("120", "8");
            decimal decResult = _genericTest.ConsumptionCulcration<decimal>(120, 8);


            /* モデルに共通事項をもたせ、違うモデルでも、同じメソッドで処理をさせる手段 */

            List<ClassAttr> classAttrs = new List<ClassAttr>()
                {   new ClassAttr(){ ClassCode = "A",Tannin="Aさん" },
                    new ClassAttr(){ ClassCode = "B",Tannin="Bさん" },
            };

            List<Education> educations = new List<Education>()
                {   new Education(){ ClassCode = "A",SeitoNo="01" },
                    new Education(){ ClassCode = "A",SeitoNo="02" },
                    new Education(){ ClassCode = "B",SeitoNo="01" },
                    new Education(){ ClassCode = "B",SeitoNo="02" },

            };
            List<ISelect> selectListClassAttrs = _genericTest.ListGenerator<ClassAttr>(classAttrs);
            List<ISelect> selectListEducations = _genericTest.ListGenerator<Education>(educations);

            selectListClassAttrs.ForEach(x => Console.WriteLine(x.ListItem));
            selectListEducations.ForEach(x => Console.WriteLine(x.ListItem));

            
        }
    }
}
