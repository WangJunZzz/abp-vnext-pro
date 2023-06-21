using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.EntityFrameworkCore;

public static class TestsDbContextModelCreatingExtensions
{
    public static void ConfigureTests(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Comment>(b =>
        {
            b.ToTable(nameof(Comment));
            b.Property(e => e.Star).HasComment("点赞");
            b.Property(e => e.Content).IsRequired().HasMaxLength(128).HasComment("内容");
            b.Property(e => e.PostId).HasComment("外键");
            b.ConfigureByConvention();
        });
        builder.Entity<Blog>(b =>
        {
            b.ToTable(nameof(Blog));
            b.Property(e => e.Code).IsRequired().HasMaxLength(128).HasComment("编码");
            b.Property(e => e.Name).IsRequired().HasMaxLength(128).HasComment("名称");
            b.ConfigureByConvention();
        });
        builder.Entity<Post>(b =>
        {
            b.ToTable(nameof(Post));
            b.Property(e => e.Name).IsRequired().HasMaxLength(128).HasComment("名称");
            b.Property(e => e.BlogId).HasComment("外键");
            b.ConfigureByConvention();
        });
    }
}
