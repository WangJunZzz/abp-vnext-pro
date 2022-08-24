using Lion.AbpPro.BasicManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProApplicationContractsModule),
        typeof(BasicManagementHttpApiClientModule),
        typeof(DataDictionaryManagementHttpApiClientModule),
        typeof(FileManagementHttpApiClientModule)
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
