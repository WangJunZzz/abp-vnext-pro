namespace Lion.AbpPro;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpLocalizationModule)
)]
public class LionAbpProLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<LionAbpProLocalizationModule>(LionAbpProLocalizationConsts.NameSpace); });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LionAbpProLocalizationResource>(LionAbpProLocalizationConsts.DefaultCultureName)
                .AddVirtualJson(LionAbpProLocalizationConsts.DefaultLocalizationResourceVirtualPath);

            options.DefaultResourceType = typeof(LionAbpProLocalizationResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace(LionAbpProLocalizationConsts.NameSpace, typeof(LionAbpProLocalizationResource)); });
    }
}