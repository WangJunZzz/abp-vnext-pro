using Volo.Abp.Modularity;
using Zzz.EntityFrameworkCore;

namespace Zzz
{
    [DependsOn(
        typeof(ZzzEntityFrameworkCoreTestModule)
        )]
    public class ZzzDomainTestModule : AbpModule
    {

    }
}