namespace Lion.AbpPro.BasicManagement.EntityFrameworkCore;

[ConnectionStringName(BasicManagementDbProperties.ConnectionStringName)]
public class BasicManagementDbContext : AbpDbContext<BasicManagementDbContext>, IBasicManagementDbContext
{
   
    public DbSet<FeatureValue> FeatureValues { get; set; }
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public BasicManagementDbContext(DbContextOptions<BasicManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBasicManagement();
    }

}
