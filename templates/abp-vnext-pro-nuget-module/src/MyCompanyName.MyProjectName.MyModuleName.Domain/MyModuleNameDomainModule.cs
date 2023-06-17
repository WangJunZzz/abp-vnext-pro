namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MyModuleNameDomainSharedModule),
        typeof(AbpCachingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAutoMapperModule)
    )]
    public class MyModuleNameDomainModule : AbpModule
    {

    }
}
