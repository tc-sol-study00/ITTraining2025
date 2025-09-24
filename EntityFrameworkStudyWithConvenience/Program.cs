using Convenience.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace EntityFrameworkStudyWithConvenience {
    internal class Program {

        private static ConvenienceContext _context;
        private static EntityFrameworkNestedObject _EFNestedObject;
        private static AutoMapperTest _AutoMapperTest;
        private static UpdateTest _UpdateTest;

        private enum Enum_Process {
            EntityFrameworkNestedObject,
            AutoMapperTest,
            UpdateTest
        }

        private static Enum_Process ProcessNo = Enum_Process.UpdateTest;

        static async Task Main(string[] args) {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ConvenienceContext");

            var optionsBuilder = new DbContextOptionsBuilder<ConvenienceContext>();
            optionsBuilder.UseNpgsql(connectionString);

            _context = new ConvenienceContext(optionsBuilder.Options);

            switch (ProcessNo) {
                case Enum_Process.EntityFrameworkNestedObject:
                    _EFNestedObject = new EntityFrameworkNestedObject(_context);
                    new Lecture20250919(_context).EfcodeSimulation();
                    break;
                case Enum_Process.AutoMapperTest:
                    _AutoMapperTest = new AutoMapperTest(_context);
                    _AutoMapperTest.AutoMapperTestExecution();
                    break;
                case Enum_Process.UpdateTest:
                    _UpdateTest = new UpdateTest(_context);
                    await _UpdateTest.DBUpdate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("設定違い");
            }
        }
    }
}

