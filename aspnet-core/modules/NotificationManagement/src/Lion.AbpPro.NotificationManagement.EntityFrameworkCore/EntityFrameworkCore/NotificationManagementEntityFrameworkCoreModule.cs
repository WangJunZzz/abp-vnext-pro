namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotificationManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class NotificationManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotificationManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}