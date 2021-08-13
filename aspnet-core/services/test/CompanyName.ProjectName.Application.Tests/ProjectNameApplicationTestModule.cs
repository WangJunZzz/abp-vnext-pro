using Volo.Abp.Modularity;

namespace CompanyName.ProjectName
{
    [DependsOn(
        typeof(ProjectNameApplicationModule),
        typeof(ProjectNameDomainTestModule)
        )]
    public class ProjectNameApplicationTestModule : AbpModule
    {

    }
}