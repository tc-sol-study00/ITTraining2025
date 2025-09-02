using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkStudy.Data {
    public class EntityFrameworkStudyContext : DbContext {
        public EntityFrameworkStudyContext(DbContextOptions<EntityFrameworkStudyContext> options)
            : base(options) {
        }

        public DbSet<EntityFrameworkStudy.Models.Education> Education { get; set; } = default!;
        public DbSet<EntityFrameworkStudy.Models.ClassAttr> ClassAttr { get; set; } = default!;

        //DBオープン用
        public static EntityFrameworkStudyContext CreateFromConfiguration() {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ASPEnshuContext");

            var optionsBuilder = new DbContextOptionsBuilder<EntityFrameworkStudyContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new EntityFrameworkStudyContext(optionsBuilder.Options);
        }
    }
}
