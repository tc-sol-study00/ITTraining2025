using EntityFrameworkStudy.Data;
using Microsoft.EntityFrameworkCore.Design;

public class EntityFrameworkStudyContextFactory : IDesignTimeDbContextFactory<EntityFrameworkStudyContext> {
    public EntityFrameworkStudyContext CreateDbContext(string[] args) {
        return EntityFrameworkStudyContext.CreateFromConfiguration();
    }
}
