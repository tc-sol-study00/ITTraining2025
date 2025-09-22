using Convenience.Data;
using Convenience.Models.DataModels;
using NuGet.Packaging;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace EntityFrameworkStudyWithConvenience {
    internal class Lecture20250919 {

        private readonly ConvenienceContext _context;

        private static readonly string ArgShiireSakiId = "A000000001";

        private static readonly string ArgChumonId = "20250917-001";
        public Lecture20250919(ConvenienceContext context) {
            _context = context;
        }
        public void EfcodeSimulation() {
            {
                //主キーを指定しているから、必ずスカラーで戻って来るが
                //リストで受けている→ダメ

                List<ChumonJisseki> chumonJisseki =
                    _context.ChumonJisseki
                    .Where(x => x.ChumonId == ArgChumonId)
                    .ToList();

                List<ChumonJissekiMeisai> chumonJissekiMeisai =
                        _context.ChumonJissekiMeisai
                        .Where(x => x.ShiireSakiId == chumonJisseki[0].ShiireSakiId && x.ChumonId == chumonJisseki[0].ChumonId)
                        .ToList();
            }

            {
                //正解例
                //スカラー変数で受ける
                ChumonJisseki? chumonJisseki =
                     _context.ChumonJisseki
                     .Where(x => x.ChumonId == ArgChumonId)
                     .FirstOrDefault();

                if (chumonJisseki != null) {    //スカラーの場合、nullになる
                    List<ChumonJissekiMeisai> chumonJissekiMeisai =
                        _context.ChumonJissekiMeisai
                        .Where(x => x.ShiireSakiId == chumonJisseki.ShiireSakiId && x.ChumonId == chumonJisseki.ChumonId)
                        .ToList();
                }

            }

            //Enermerable
            {
                //ToListの段階で実行がかかり、全件がchumonjissekisに入る（100万件が対象なら100万件）
                List<ChumonJisseki> chumonjissekis = _context.ChumonJisseki.ToList();

                foreach (ChumonJisseki aChumonJisseki in chumonjissekis) {
                    Console.WriteLine(aChumonJisseki.ChumonId);
                }
            }

            {
                //まだ実行がかかっていない
                IEnumerable<ChumonJisseki> chumonjissekis = _context.ChumonJisseki;
                chumonjissekis=chumonjissekis.Where(x => x.ChumonId == ArgChumonId);

                //foreachで１件目を取り出して、aChumonJissekiに都度格納
                //メモリの節約など
                foreach (ChumonJisseki aChumonJisseki in chumonjissekis) {
                    Console.WriteLine(aChumonJisseki.ChumonId);
                }


            }

            {
                //なぜ、IListを使うか
                //IEnumerable→ICollection→IList
                //Ilist->List

                IList<ChumonJisseki> data = _context.ChumonJisseki.ToList();

                List2<ChumonJisseki> data2 = _context.ChumonJisseki.ToList();

                IList<ChumonJisseki> data3 = data2;


                Collection<int> a;



            }
        }


        public interface IList2<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable {
            // IList<T> の他に独自の機能を追加することも可能
            void PrintAll();
        }

        public class List2<T> : IList2<T> {
            private readonly List<T> _inner = new();

            public List2(IEnumerable<T> collection) => _inner = new List<T>(collection);

            // 暗黙変換（List<T> → List2<T>）
            public static implicit operator List2<T>(List<T> list) {
                return new List2<T>(list);
            }

            // 必要なら逆変換（List2<T> → List<T>）
            //public static implicit operator List<T>(List2<T> list2) {
            //    return list2._inner;
            //}


            // 独自メソッド
            public void PrintAll() {
                foreach (var item in _inner) {
                    if (item == null) continue;

                    Console.WriteLine($"--- {typeof(T).Name} ---");

                    foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                        // 属性を取得（例：DisplayNameAttribute）
                        var displayAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
                        var displayName = displayAttr?.DisplayName ?? prop.Name; // 属性があれば使う、なければプロパティ名

                        // 値を取得
                        var value = prop.GetValue(item);

                        Console.WriteLine($"{displayName}: {value}");
                    }
                }
            }

            // IList<T> の実装を List<T> に委譲
            public T this[int index] { get => _inner[index]; set => _inner[index] = value; }

            public int Count => _inner.Count;
            public bool IsReadOnly => ((ICollection<T>)_inner).IsReadOnly;

            public void Add(T item) => _inner.Add(item);
            public void Clear() => _inner.Clear();
            public bool Contains(T item) => _inner.Contains(item);
            public void CopyTo(T[] array, int arrayIndex) => _inner.CopyTo(array, arrayIndex);
            public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();
            public int IndexOf(T item) => _inner.IndexOf(item);
            public void Insert(int index, T item) => _inner.Insert(index, item);
            public bool Remove(T item) => _inner.Remove(item);
            public void RemoveAt(int index) => _inner.RemoveAt(index);
            IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
        }
    }
}
