namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementDomainModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule)
        )]
    public class LanguageManagementApplicationModule : AbpModule
    {
    }
}
