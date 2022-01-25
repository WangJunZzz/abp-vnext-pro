using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileManagementDomainSharedModule)
)]
public class FileManagementDomainModule : AbpModule
{
}