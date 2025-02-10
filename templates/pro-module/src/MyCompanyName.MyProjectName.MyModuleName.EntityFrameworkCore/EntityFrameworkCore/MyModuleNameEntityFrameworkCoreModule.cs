namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    [DependsOn(
        typeof(MyModuleNameDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MyModuleNameEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MyModuleNameDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}