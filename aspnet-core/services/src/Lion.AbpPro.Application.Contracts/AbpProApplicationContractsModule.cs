using Lion.AbpPro.CodeManagement;
using Lion.AbpPro.TemplateManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(BasicManagementApplicationContractsModule),
        typeof(DataDictionaryManagementApplicationContractsModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(CodeManagementApplicationContractsModule),
        typeof(TemplateManagementApplicationContractsModule)
    )]
    public class AbpProApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProDtoExtensions.Configure();
        }
    }
}
