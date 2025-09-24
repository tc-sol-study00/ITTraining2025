using Convenience.Data;
using Convenience.Migrations;
using Convenience.Models.DataModels;
using Convenience.Models.Interfaces;
using Convenience.Models.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudyWithConvenience {
    internal class UpdateTest {
        private readonly ConvenienceContext _context;
        private readonly Chumon _chumon;
        private readonly Shiire _shiire;

        private static readonly string ChumonIdKey = "20250917-001";

        private static readonly string ShiireSakiIdKey = "A000000001";


        public UpdateTest(ConvenienceContext context) {
            _context = context;
        }

        public async Task DBUpdate() {

            Dictionary<string, EntityState> StateDictionary = new Dictionary<string, EntityState>();

            _context.ChangeTracker.Clear();

            ChumonJisseki? chumonJissekis
                = _context.ChumonJisseki
                    //.AsNoTracking()
                    .Where(chj => chj.ChumonId == ChumonIdKey)
                    .Include(chj => chj.ChumonJissekiMeisais)
                    .FirstOrDefault();

            if (chumonJissekis != null) {

                if (chumonJissekis.ChumonJissekiMeisais != null) {


                    Console.WriteLine("1.Before----------");
                    Console.WriteLine(_context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).ToString());
                    foreach (var data in _context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).Properties) {
                        Console.WriteLine($"{data.Metadata.Name}: {data.OriginalValue}=>{data.CurrentValue} (Modified={data.IsModified})");
                    }

                    chumonJissekis.ChumonJissekiMeisais[0].ChumonSu = 11.0m;

                    Console.WriteLine("1.After ----------");
                    Console.WriteLine(_context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).ToString());
                    foreach (var data in _context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).Properties) {
                        Console.WriteLine($"{data.Metadata.Name}: {data.OriginalValue}=>{data.CurrentValue} (Modified={data.IsModified})");
                    }

                    Console.WriteLine("2.Before----------");
                    Console.WriteLine(_context.Entry(chumonJissekis.ChumonJissekiMeisais[1]).ToString());
                    foreach (var data in _context.Entry(chumonJissekis.ChumonJissekiMeisais[1]).Properties) {
                        Console.WriteLine($"{data.Metadata.Name}: {data.OriginalValue}=>{data.CurrentValue} (Modified={data.IsModified})");
                    }

                    _context.Update(chumonJissekis.ChumonJissekiMeisais[1]);

                    Console.WriteLine("2.After ----------");
                    Console.WriteLine(_context.Entry(chumonJissekis.ChumonJissekiMeisais[1]).ToString());
                    foreach (var data in _context.Entry(chumonJissekis.ChumonJissekiMeisais[1]).Properties) {
                        Console.WriteLine($"{data.Metadata.Name}: {data.OriginalValue}=>{data.CurrentValue} (Modified={data.IsModified})");
                    }
                    Console.WriteLine(_context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).ToString());
                    foreach (var data in _context.Entry(chumonJissekis.ChumonJissekiMeisais[0]).Properties) {
                        Console.WriteLine($"{data.Metadata.Name}: {data.OriginalValue}=>{data.CurrentValue} (Modified={data.IsModified})");
                    }

                    Console.WriteLine("3.ChangeTracker ----------");
                    foreach (var entrydt in _context.ChangeTracker.Entries()) {
                        Console.WriteLine($"{entrydt.ToString()}");
                    }

                    _context.ChangeTracker.Clear();

                    ChumonJisseki dt = new ChumonJisseki() { ChumonId = "20250924-010", ShiireSakiId = "A000000001" };
                    _context.Add(dt);

                    Console.WriteLine("4.----------------");
                    var entry=_context.Entry(dt);
                    Console.WriteLine($"{entry.ToString()}");


                }
            }
        }
    }
}
