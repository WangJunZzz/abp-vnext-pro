namespace Lion.AbpPro.FileManagement.EntityFrameworkCore;

public static class FileManagementDbContextModelCreatingExtensions
{
    public static void ConfigureFileManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<Lion.AbpPro.FileManagement.Files.File>(b =>
        {
            b.ToTable(FileManagementDbProperties.DbTablePrefix + nameof(Lion.AbpPro.FileManagement.Files.File), FileManagementDbProperties.DbSchema);
            b.HasIndex(q => q.FileName);
            b.HasIndex(q => q.CreationTime);
            b.ConfigureByConvention();
        });
    }
}