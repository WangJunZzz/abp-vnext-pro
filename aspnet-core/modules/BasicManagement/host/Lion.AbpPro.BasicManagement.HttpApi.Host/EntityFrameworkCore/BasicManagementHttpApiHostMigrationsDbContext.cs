using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lion.AbpPro.BasicManagement.EntityFrameworkCore;

public class BasicManagementHttpApiHostMigrationsDbContext : AbpDbContext<BasicManagementHttpApiHostMigrationsDbContext>
{
    public BasicManagementHttpApiHostMigrationsDbContext(DbContextOptions<BasicManagementHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBasicManagement();
    }
}
