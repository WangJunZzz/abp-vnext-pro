namespace Lion.AbpPro.BasicManagement.EntityFrameworkCore;

public static class BasicManagementDbContextModelCreatingExtensions
{
    public static void ConfigureBasicManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
    }
}
