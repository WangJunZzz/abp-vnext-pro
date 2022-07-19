namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FileManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FileManagementApplicationContractsModule).Assembly,
            FileManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<FileManagementHttpApiClientModule>(); });
    }
}