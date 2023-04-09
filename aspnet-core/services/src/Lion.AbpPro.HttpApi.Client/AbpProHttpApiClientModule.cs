using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.LanguageManagement;
using Lion.AbpPro.NotificationManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProApplicationContractsModule),
        typeof(BasicManagementHttpApiClientModule),
        typeof(DataDictionaryManagementHttpApiClientModule),
        typeof(NotificationManagementHttpApiClientModule),
        typeof(LanguageManagementHttpApiClientModule)
    )]
    public class AbpProHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpProApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
