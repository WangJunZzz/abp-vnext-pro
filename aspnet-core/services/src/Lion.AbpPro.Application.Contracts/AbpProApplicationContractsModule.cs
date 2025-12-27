using Lion.AbpPro.Ddd.Application.Contracts;
using Lion.AbpPro.FileManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(BasicManagementApplicationContractsModule),
        typeof(DataDictionaryManagementApplicationContractsModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(FileManagementApplicationContractsModule),
        typeof(AbpProDddApplicationContractsModule)
    )]
    public class AbpProApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProDtoExtensions.Configure();
        }
    }
}
