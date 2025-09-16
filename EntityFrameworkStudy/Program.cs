using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkStudy {

    class Program {

        public static EntityFrameworkStudyContext _context;

        private enum EnumProcessSelect {
            KougiYouMethod,
            EnshuMethod,
            LinqJoin
        }

        private static readonly EnumProcessSelect ProcessSelect 
            = EnumProcessSelect.LinqJoin;

        static void Main() {
            //DBコンテキストの生成
            _context = EntityFrameworkStudyContext.CreateFromConfiguration();

            switch (ProcessSelect) {
                case EnumProcessSelect.KougiYouMethod:
                    new KougiYou(_context).KougiYouMethod();
                    break;
                case EnumProcessSelect.EnshuMethod:
                    new Enshu(_context).EnshuMethod();
                    break;
                case EnumProcessSelect.LinqJoin:
                    new LinqJoin(_context).LinqJoinTest();
                    break;
            }
        }
    }
}