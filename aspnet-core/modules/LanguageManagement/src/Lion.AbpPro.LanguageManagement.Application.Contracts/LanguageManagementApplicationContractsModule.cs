namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class LanguageManagementApplicationContractsModule : AbpModule
    {

    }
}
