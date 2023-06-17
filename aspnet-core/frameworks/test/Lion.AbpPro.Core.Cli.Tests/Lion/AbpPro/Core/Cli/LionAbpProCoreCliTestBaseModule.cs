using Lion.AbpPro.Cli;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using LionAbpProOptions = Lion.AbpPro.Cli.Options.LionAbpProOptions;

namespace Lion.AbpPro.Core.Cli
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpProCliCoreModule))]
    public class LionAbpProCoreCliTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            Configure<LionAbpProOptions>(options =>
            {
                options.Owner = "WangJunZzz";
                options.RepositoryId = "abp-vnext-pro";
                options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            });
        }
    }
}