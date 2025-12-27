using Lion.AbpPro.Demo;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Lion.AbpPro.EntityFrameworkCore;

public static class AbpProDbContextModelCreatingExtensions
{
    public static void ConfigureAbpPro(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        
        builder.Entity<DemoAggregate>(b =>
        {
            b.ToTable(AbpProDbProperties.DbTablePrefix + "Demos", DataDictionaryManagementDbProperties.DbSchema);
            b.ConfigureByConvention();
        });
    }
}