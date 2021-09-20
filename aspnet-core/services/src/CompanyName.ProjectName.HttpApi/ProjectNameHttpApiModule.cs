using CompanyName.ProjectName.DataDictionaryManagement;
using Localization.Resources.AbpUi;
using CompanyName.ProjectName.Localization;
using CompanyName.ProjectName.NotificationManagement;
using CompanyName.ProjectName.QueryManagement;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace CompanyName.ProjectName
{
    [DependsOn(
        typeof(ProjectNameApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(DataDictionaryManagementHttpApiModule),
        typeof(NotificationManagementHttpApiModule),
        typeof(QueryManagementHttpApiModule)
        )]
    public class ProjectNameHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ProjectNameResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
