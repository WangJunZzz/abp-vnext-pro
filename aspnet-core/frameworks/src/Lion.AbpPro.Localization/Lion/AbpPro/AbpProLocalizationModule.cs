namespace Lion.AbpPro;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpLocalizationModule)
)]
public class AbpProLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<AbpProLocalizationModule>(AbpProLocalizationConsts.NameSpace); });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpProLocalizationResource>(AbpProLocalizationConsts.DefaultCultureName)
                .AddVirtualJson(AbpProLocalizationConsts.DefaultLocalizationResourceVirtualPath);

            options.DefaultResourceType = typeof(AbpProLocalizationResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace(AbpProLocalizationConsts.NameSpace, typeof(AbpProLocalizationResource)); });
    }
}