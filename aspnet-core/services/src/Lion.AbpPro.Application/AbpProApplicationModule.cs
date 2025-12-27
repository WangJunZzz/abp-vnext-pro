using Lion.AbpPro.Ddd.Application;
using Lion.AbpPro.FileManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainModule),
        typeof(AbpProApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(FileManagementApplicationModule),
        typeof(AbpProDddApplicationModule)
    )]
    public class AbpProApplicationModule : AbpModule
    {
    }
}