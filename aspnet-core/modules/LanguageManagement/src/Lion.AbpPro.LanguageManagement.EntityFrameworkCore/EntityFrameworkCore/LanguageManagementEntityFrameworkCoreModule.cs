namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(LanguageManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class LanguageManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LanguageManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}