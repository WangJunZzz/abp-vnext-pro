using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.EntityFrameworkCore;

public class TestsHttpApiHostMigrationsDbContext : AbpDbContext<TestsHttpApiHostMigrationsDbContext>
{
    public TestsHttpApiHostMigrationsDbContext(DbContextOptions<TestsHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

     
    }
}
