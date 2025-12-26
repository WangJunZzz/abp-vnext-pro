namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MyModuleNameDomainSharedModule),
        typeof(AbpCachingModule)
    )]
    public class MyModuleNameDomainModule : AbpModule
    {
    }
}
