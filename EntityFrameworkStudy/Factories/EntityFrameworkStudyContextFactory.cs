using EntityFrameworkStudy.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkStudy.Factories {
    public class EntityFrameworkStudyContextFactory : IDesignTimeDbContextFactory<EntityFrameworkStudyContext> {
        public EntityFrameworkStudyContext CreateDbContext(string[] args) {
            return EntityFrameworkStudyContext.CreateFromConfiguration();
        }
    }
}