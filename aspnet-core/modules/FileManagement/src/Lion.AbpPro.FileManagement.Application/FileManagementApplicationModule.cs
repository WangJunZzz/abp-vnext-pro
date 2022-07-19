namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementDomainModule),
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBlobStoringAliyunModule)
)]
public class FileManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileManagementApplicationModule>();
        ConfigureBlobStoringAliyun(context);
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<FileManagementApplicationModule>(true); });
    }
    
    private void ConfigureBlobStoringAliyun(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseAliyun(aliyun =>
                {
                    aliyun.AccessKeyId =
                        configuration.GetValue<string>("AliYun:OSS:AccessKeyId");
                    aliyun.AccessKeySecret =
                        configuration.GetValue<string>("AliYun:OSS:AccessKeySecret");
                    aliyun.Endpoint = configuration.GetValue<string>("AliYun:OSS:Endpoint");
                    aliyun.RegionId = configuration.GetValue<string>("AliYun:OSS:RegionId");
                    aliyun.ContainerName =
                        configuration.GetValue<string>("AliYun:OSS:ContainerName");
                });
            });
        });
    }
}