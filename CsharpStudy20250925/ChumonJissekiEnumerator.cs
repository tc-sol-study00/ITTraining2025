using Convenience.Data;
using Convenience.Models.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; // OrderBy, ThenBy を使う場合必要

namespace CsharpStudy20250925 {
    internal class ChumonJissekiEnumerator : IEnumerable<ChumonJissekiMeisai> {

        private readonly ConvenienceContext _context;

        public ChumonJissekiEnumerator(ConvenienceContext context) {
            this._context = context;
        }

        // IEnumerable<T> の実装
        //イテレーターという
        public IEnumerator<ChumonJissekiMeisai> GetEnumerator() {
            int counter = 0;
            foreach (var item in _context.ChumonJissekiMeisai
                                         .OrderBy(cm => cm.ChumonId)
                                         .ThenBy(cm => cm.ShiirePrdId)) {
                yield return item;
            }
        }

        // IEnumerable の実装（非ジェネリック版）
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void DisplayData<T>(T argMeisai) where T : class {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            int widthMax = 0;
            foreach (PropertyInfo item in propertyInfos) {

                string rec = $"{item.Name}={item.GetValue(argMeisai) ?? "null"}";

                int width = CharLength(rec);
                widthMax = width > widthMax ? width : widthMax;
                widthMax=Math.Max(width, widthMax);

                Console.WriteLine(rec);
            }
            Console.WriteLine(new string('-', widthMax));
        }

        private int CharLength(string argRec) {
            int width = 0;
            foreach (char c in argRec) {

                switch (c) {
                    case <= '\x7F':
                        width += 1;
                        break;
                    case >= '\xFF61' and <= '\xFF9F':
                        width += 1;
                        break;
                    default:
                        width += 2;
                        break;
                }
                
                if (c <= '\x7F') {
                    width += 1;
                }
                else if (c >= '\xFF61' && c <= '\xFF9F') {
                    width += 1;
                }
                else {
                    width += 2;
                }

                //switch式
                width += c switch {
                    <= '\x7F' => 1,                         // ASCII
                    >= '\xFF61' and <= '\xFF9F' => 1,       // 半角カナ
                    _ => 2                                  // 漢字など
                };
            }

            return width;

        }
    }
}