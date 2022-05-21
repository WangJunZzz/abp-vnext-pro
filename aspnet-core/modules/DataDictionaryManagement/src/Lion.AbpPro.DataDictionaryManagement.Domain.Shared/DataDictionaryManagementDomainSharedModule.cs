using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Lion.AbpPro.DataDictionaryManagement.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class DataDictionaryManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DataDictionaryManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DataDictionaryManagementResource>(DataDictionaryManagementConsts.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/DataDictionaryManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(DataDictionaryManagementConsts.NameSpace, typeof(DataDictionaryManagementResource));
            });
        }
    }
}
