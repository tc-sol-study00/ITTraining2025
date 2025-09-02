using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;




namespace EntityFrameworkStudy {
    
    class Program {

        public static EntityFrameworkStudyContext _context;

        static void Main() {
            //DBコンテキストの生成
            _context = EntityFrameworkStudyContext.CreateFromConfiguration();

            new KougiYou(_context).KougiYouMethod();
            //new Enshu(_context).EnshuMethod();


        }
    }
}