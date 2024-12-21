using Lion.AbpPro.CodeManagement.EntityFrameworkCore;
using Lion.AbpPro.LanguageManagement.EntityFrameworkCore;
using Lion.AbpPro.TemplateManagement.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace Lion.AbpPro.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpProDomainModule),
        typeof(BasicManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(DataDictionaryManagementEntityFrameworkCoreModule),
        typeof(NotificationManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementEntityFrameworkCoreModule),
        typeof(CodeManagementEntityFrameworkCoreModule),
        typeof(TemplateManagementEntityFrameworkCoreModule)
        )]
    public class AbpProEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AbpProDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            
            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also AbpProMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });
        }
    }
}
