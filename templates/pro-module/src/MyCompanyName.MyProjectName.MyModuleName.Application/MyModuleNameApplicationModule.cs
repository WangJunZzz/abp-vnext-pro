namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(MyModuleNameDomainModule),
        typeof(MyModuleNameApplicationContractsModule),
        typeof(AbpDddApplicationModule)
        )]
    public class MyModuleNameApplicationModule : AbpModule
    {
     
    }
}
