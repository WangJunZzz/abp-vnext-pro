using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Lion.AbpPro.EntityFrameworkCore.Tests.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Data;

public class TestsDbContext : AbpDbContext<TestsDbContext>
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public TestsDbContext(DbContextOptions<TestsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
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
        /* Configure your own entities here */
    }
}