namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementDomainModule),
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
)]
public class FileManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<FileManagementApplicationModule>(true); });
    }
}