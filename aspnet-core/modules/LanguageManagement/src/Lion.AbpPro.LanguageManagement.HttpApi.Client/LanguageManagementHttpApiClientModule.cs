namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class LanguageManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "LanguageManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(LanguageManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
