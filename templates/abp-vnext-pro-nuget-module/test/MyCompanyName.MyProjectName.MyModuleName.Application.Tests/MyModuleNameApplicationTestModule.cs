namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(MyModuleNameApplicationModule),
        typeof(MyModuleNameDomainTestModule)
        )]
    public class MyModuleNameApplicationTestModule : AbpModule
    {

    }
}
