using Zzz.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Zzz
{
    [DependsOn(
        typeof(ZzzEntityFrameworkCoreTestModule)
        )]
    public class ZzzDomainTestModule : AbpModule
    {

    }
}