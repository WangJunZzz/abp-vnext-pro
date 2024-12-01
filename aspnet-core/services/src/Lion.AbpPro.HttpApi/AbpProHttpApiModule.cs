using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.CodeManagement;
using Lion.AbpPro.LanguageManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProApplicationContractsModule),
        typeof(BasicManagementHttpApiModule),
        typeof(DataDictionaryManagementHttpApiModule),
        typeof(NotificationManagementHttpApiModule),
        typeof(LanguageManagementHttpApiModule),
        typeof(CodeManagementHttpApiModule)
        )]
    public class AbpProHttpApiModule : AbpModule
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
                    .Get<AbpProResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
