namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(MyProjectNameApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(LanguageManagementApplicationModule)
        )]
    public class MyProjectNameApplicationModule : AbpModule
    {
      
    }
}
