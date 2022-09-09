using Lion.AbpPro.BasicManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(BasicManagementApplicationContractsModule),
        typeof(DataDictionaryManagementApplicationContractsModule)
    )]
    public class AbpProApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProDtoExtensions.Configure();
        }
    }
}
