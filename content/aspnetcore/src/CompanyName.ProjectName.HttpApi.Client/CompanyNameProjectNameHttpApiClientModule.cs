using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace CompanyNameProjectName
{
    [DependsOn(
        typeof(CompanyNameProjectNameApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class CompanyNameProjectNameHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "CompanyNameProjectName";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(CompanyNameProjectNameApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
