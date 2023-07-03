using Lion.AbpPro.Cli;
using Lion.AbpPro.Cli.Options;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Core.Cli
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpProCliCoreModule))]
    public class AbpProCoreCliTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            Configure<AbpProCliOptions>(options =>
            {
                options.Owner = "WangJunZzz";
                options.RepositoryId = "abp-vnext-pro";
                options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            });
        }
    }
}