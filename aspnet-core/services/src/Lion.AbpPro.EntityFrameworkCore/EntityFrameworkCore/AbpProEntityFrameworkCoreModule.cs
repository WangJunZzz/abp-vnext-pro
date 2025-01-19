using Lion.AbpPro.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace Lion.AbpPro.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpProDomainModule),
        typeof(BasicManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(DataDictionaryManagementEntityFrameworkCoreModule),
        typeof(NotificationManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementEntityFrameworkCoreModule)
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
                 * See also HayoonKoreaDbContextFactory for EF Core tooling.
                 *  https://github.com/abpframework/abp/issues/21879
                 * */
                options.UseMySQL(builder =>
                {
                    builder.TranslateParameterizedCollectionsToConstants();
                });
            });
        }
    }
}
