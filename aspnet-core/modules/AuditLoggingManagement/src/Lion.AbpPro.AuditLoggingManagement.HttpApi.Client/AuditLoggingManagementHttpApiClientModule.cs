using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.AuditLoggingManagement
{
    public class AuditLoggingManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "AuditLoggingManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AuditLoggingManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}