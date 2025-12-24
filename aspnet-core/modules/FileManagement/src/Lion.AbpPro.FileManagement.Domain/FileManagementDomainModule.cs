namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileManagementDomainSharedModule),
    typeof(AbpBlobStoringModule)
)]
public class FileManagementDomainModule : AbpModule
{
}