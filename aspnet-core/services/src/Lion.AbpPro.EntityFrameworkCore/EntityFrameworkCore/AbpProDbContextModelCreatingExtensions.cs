using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Lion.AbpPro.EntityFrameworkCore;

public static class AbpProDbContextModelCreatingExtensions
{
    public static void ConfigureAbpPro(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}