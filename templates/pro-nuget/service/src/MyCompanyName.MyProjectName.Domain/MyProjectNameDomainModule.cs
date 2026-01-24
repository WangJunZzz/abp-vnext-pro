namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(BasicManagementDomainModule),
        typeof(NotificationManagementDomainModule),
        typeof(DataDictionaryManagementDomainModule),
        typeof(LanguageManagementDomainModule)
    )]
    public class MyProjectNameDomainModule : AbpModule
    {
    
    }
}