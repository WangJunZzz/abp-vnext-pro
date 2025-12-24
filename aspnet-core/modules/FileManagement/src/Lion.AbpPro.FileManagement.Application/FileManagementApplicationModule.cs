namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementDomainModule),
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class FileManagementApplicationModule : AbpModule
{
}