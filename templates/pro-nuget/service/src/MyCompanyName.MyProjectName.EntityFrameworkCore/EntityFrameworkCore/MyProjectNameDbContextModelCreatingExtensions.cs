using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

public static class MyProjectNameDbContextModelCreatingExtensions
{
    public static void ConfigureMyProjectName(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}