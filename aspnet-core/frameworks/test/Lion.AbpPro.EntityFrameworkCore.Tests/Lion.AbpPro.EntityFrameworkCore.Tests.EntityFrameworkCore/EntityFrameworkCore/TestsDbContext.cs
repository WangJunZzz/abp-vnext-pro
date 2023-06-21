using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.EntityFrameworkCore;

[ConnectionStringName(TestsDbProperties.ConnectionStringName)]
public class TestsDbContext : AbpDbContext<TestsDbContext>, ITestsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
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

        builder.ConfigureTests();
    }
}