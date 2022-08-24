using Volo.Abp.Modularity;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(BasicManagementApplicationModule),
    typeof(BasicManagementDomainTestModule)
    )]
public class BasicManagementApplicationTestModule : AbpModule
{

}
