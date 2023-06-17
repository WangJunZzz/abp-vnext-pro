using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Cli;

[DependsOn(
    typeof(Lion.AbpPro.Cli.AbpProCliCoreModule),
    typeof(AbpAutofacModule)
)]
public class AbpProCliModule : AbpModule
{
}
