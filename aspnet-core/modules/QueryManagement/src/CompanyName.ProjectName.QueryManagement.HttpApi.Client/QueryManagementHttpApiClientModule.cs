using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement
{
    [DependsOn(
        typeof(QueryManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class QueryManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "QueryManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(QueryManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
