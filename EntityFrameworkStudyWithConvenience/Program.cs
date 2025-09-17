using Convenience.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkStudyWithConvenience {
    internal class Program {

        private static ConvenienceContext _context;
        private static EntityFrameworkNestedObject _EFNestedObject;
        
        static void Main(string[] args) {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ConvenienceContext");

            var optionsBuilder = new DbContextOptionsBuilder<ConvenienceContext>();
            optionsBuilder.UseNpgsql(connectionString);

            _context = new ConvenienceContext(optionsBuilder.Options);

            _EFNestedObject = new EntityFrameworkNestedObject(_context);
        }
    }
}
