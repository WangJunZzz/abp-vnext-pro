namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(AbpCachingModule)
    )]
    public class DataDictionaryManagementDomainModule : AbpModule
    {
    }
}
