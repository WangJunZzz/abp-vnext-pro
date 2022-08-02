using Lion.AbpPro.Roles;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpProApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(FileManagementApplicationModule),
        typeof(AbpProFreeSqlModule),
        typeof(AbpBackgroundJobsHangfireModule)
        )]
    public class AbpProApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpProApplicationModule>();
            });
            
            Configure<PermissionOptions>(options =>
            {
                options.Excludes.Add("AbpIdentity.Users.ManagePermissions");
                options.Excludes.Add("AbpIdentity.UserLookup");
                options.Excludes.Add("FeatureManagement");
                options.Excludes.Add("FeatureManagement.ManageHostFeatures");
                options.Excludes.Add("SettingManagement");
                options.Excludes.Add("SettingManagement.Emailing");
                options.Excludes.Add("AbpTenantManagement");
                options.Excludes.Add("AbpTenantManagement.Tenants.ManageFeatures");
                options.Excludes.Add("AbpTenantManagement.Tenants.ManageConnectionStrings");
            });
        }
    }
}
