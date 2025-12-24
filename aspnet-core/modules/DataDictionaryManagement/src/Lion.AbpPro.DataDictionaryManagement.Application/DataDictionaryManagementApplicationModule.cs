namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(DataDictionaryManagementDomainModule),
        typeof(DataDictionaryManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule)
        )]
    public class DataDictionaryManagementApplicationModule : AbpModule
    {
    }
}
