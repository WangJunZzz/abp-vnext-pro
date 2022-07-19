namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FileManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder => { mvcBuilder.AddApplicationPartIfNotExists(typeof(FileManagementHttpApiModule).Assembly); });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FileManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}