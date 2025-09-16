using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkStudy {
    public class Enshu {

        private static EntityFrameworkStudyContext _context;

        public Enshu(EntityFrameworkStudyContext context) {
            _context = context;
        }

        public void EnshuMethod() {

            //演習１－１：foreachステートメントを使う場合(delegateを使った例)
            Action<Education> m1 = item => {
                Console.WriteLine("{0,-5}:{1,-5}:{2,-5}:{3,5}:{4,5}:{5,5}:{6:6}", item.ClassCode, item.ClassAttr.Tannin, item.SeitoNo, item.KokugoScore, item.SuugakuScore, item.SuugakuScore,
                                           item.KokugoScore + item.SuugakuScore + item.SuugakuScore);
            };

            //SQLの中身を見る様（処理には関係がない）
            var a = _context.Education.Include(x => x.ClassAttr).Where(x => x.RikaScore > 10);
            var b = a.OrderBy(x => x.ClassCode);
            var c = b.ThenBy(x => x.SeitoNo);
            var d = c.ToList();


            List<Education> dataList = d;

            //var result = _context.Education.Include(x => x.ClassAttr).OrderBy(x => x.ClassCode).ThenBy(x => x.SeitoNo);
            foreach (var item in dataList) {
                m1(item);
            }

            //以下のようにdelegateの式を置き換えることが可能(m1がメソッド用の変数だから）

            m1 = item => { uint ttl = item.KokugoScore ?? 0 + item.SuugakuScore ?? 0 + item.SuugakuScore ?? 0; Console.WriteLine("{0,-5}:{1,-5}:{2,-5}:{3,5}:{4,5}:{5,5}:{6:6}", item.ClassCode, item.ClassAttr.Tannin, item.SeitoNo, item.KokugoScore, item.SuugakuScore, item.SuugakuScore, ttl); };

            //演習１－２(delegateを使った例：x => m1(x)の部分
            _context.Education.Include(x => x.ClassAttr).OrderBy(x => x.ClassCode).ThenBy(x => x.SeitoNo).ToList().ForEach(x => Console.WriteLine(x.ClassCode,x));


            //Option
            //Func<Education, Education> f1 = x => { x.KokugoScore += 100; return x; };

            //var dataList2 = _context.Education.Include(x => x.ClassAttr).OrderBy(x => x.ClassCode).ThenBy(x => x.SeitoNo).ToList().ForEach(x => f1(x));

            if (true) {
                //演習２
                //挿入したいデータの設定

                Education setDataToEducation = new Education { ClassCode = "A", SeitoNo = "999", KokugoScore = 99, SuugakuScore = 99, RikaScore = 99 };

                if (_context.Education.Find(setDataToEducation.ClassCode, setDataToEducation.SeitoNo)==null) {
                    _context.Education.Add(setDataToEducation);
                    _context.SaveChanges();
                }

                //問い合わせ
                _context.Education.ToList().ForEach(x => m1(x));
            }

            //演習３
            //削除
            //Findは主キーの値をいれるが、複合主キーの場合は使えない
            //Education deleteObject = _context.Education.Find(x => x.ClassCode == "A" && x.SeitoNo == "999");
            //FirstOrDefaultは、かならず１行を返す、データがない場合はnull
            if (false) {
                Education deleteObject = _context.Education.FirstOrDefault(x => x.ClassCode == "A" && x.SeitoNo == "999");

                 if (deleteObject != null) {
                    _context.Education.Remove(deleteObject);
                    _context.SaveChanges();
                }
            }

            if (false) {
                //Findを使った例
                Education deleteObject2=_context.Education.Find( "A", "999" );  //主キーの値をセット
                if (deleteObject2 != null) {
                    _context.Education.Remove(deleteObject2);
                    _context.SaveChanges();
                }
            }

            if (true) {

                //演習４
                //更新
                //リスト形式パターン
                List<Education> educationData = _context.Education.Where(x => x.ClassCode == "A" && x.SeitoNo == "999").ToList();
                educationData.ForEach(x => x.KokugoScore = 100);

                //主キー指定で一行のみ削除のパターン（１件削除パターン）
                Education educationData2 = _context.Education.Where(x => x.ClassCode == "A" && x.SeitoNo == "999").FirstOrDefault();
                if (educationData2 != null) {
                    educationData2.KokugoScore = 100;
                }
                _context.SaveChanges(); //updateはLINQ問い合わせの結果を更新して、SaveChangeを発行すれば更新がかかる
                                        //EF6が更新差分を記憶しているため、余計なupdate分は発行しない
            }
        }


    }
}

