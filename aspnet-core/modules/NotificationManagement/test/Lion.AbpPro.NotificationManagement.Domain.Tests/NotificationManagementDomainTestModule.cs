namespace Lion.AbpPro.NotificationManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(NotificationManagementEntityFrameworkCoreTestModule)
        )]
    public class NotificationManagementDomainTestModule : AbpModule
    {
        
    }
}
