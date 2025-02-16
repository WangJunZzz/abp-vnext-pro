namespace Lion.AbpPro.FileManagement.EntityFrameworkCore;

public static class FileManagementDbContextModelCreatingExtensions
{
    public static void ConfigureFileManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<FileObject>(b =>
        {
            b.ToTable(FileManagementDbProperties.DbTablePrefix + "FileObjects");
            b.Property(e => e.ProviderKey).IsRequired().HasMaxLength(128).HasComment("文件Provider");
            b.Property(e => e.FileExtension).IsRequired().HasMaxLength(36).HasComment("文件扩展名");
            b.Property(e => e.Bytes).HasComment("二进制数据");
            b.Property(e => e.FileSize).HasComment("文件大小");
            b.Property(e => e.ContentType).IsRequired().HasMaxLength(128).HasComment("文件名称");
            b.Property(e => e.FileName).IsRequired().HasMaxLength(128).HasComment("文件名称");
            b.HasIndex(e => e.FileName);
            b.ConfigureByConvention();
        });
    }
}