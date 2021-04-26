using Volo.Abp.Modularity;
using CompanyNameProjectName.EntityFrameworkCore;

namespace CompanyNameProjectName
{
    [DependsOn(
        typeof(CompanyNameProjectNameEntityFrameworkCoreTestModule)
        )]
    public class CompanyNameProjectNameDomainTestModule : AbpModule
    {

    }
}