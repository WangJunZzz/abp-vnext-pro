using CompanyName.ProjectName.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName
{
    [DependsOn(
        typeof(ProjectNameEntityFrameworkCoreTestModule)
        )]
    public class ProjectNameDomainTestModule : AbpModule
    {

    }
}