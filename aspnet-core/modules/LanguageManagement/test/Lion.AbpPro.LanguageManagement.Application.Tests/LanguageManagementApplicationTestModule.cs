namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementApplicationModule),
        typeof(LanguageManagementDomainTestModule)
        )]
    public class LanguageManagementApplicationTestModule : AbpModule
    {

    }
}
