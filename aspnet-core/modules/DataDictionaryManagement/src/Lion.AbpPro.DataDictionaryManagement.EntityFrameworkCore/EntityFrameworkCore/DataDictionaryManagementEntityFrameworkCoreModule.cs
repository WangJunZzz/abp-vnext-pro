namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(DataDictionaryManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class DataDictionaryManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DataDictionaryManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}