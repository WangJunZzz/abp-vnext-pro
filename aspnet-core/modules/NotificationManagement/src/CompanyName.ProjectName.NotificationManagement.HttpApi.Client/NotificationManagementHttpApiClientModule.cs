using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NotificationManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "NotificationManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
