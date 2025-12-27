using Lion.AbpPro.Demo;

namespace Lion.AbpPro.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IAbpProDbContext : IEfCoreDbContext
    {
            DbSet<DemoAggregate> Demos { get; set; }
    }
}
