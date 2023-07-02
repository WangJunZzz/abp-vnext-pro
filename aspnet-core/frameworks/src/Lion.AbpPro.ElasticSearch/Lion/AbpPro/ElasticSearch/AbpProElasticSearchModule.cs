namespace Lion.AbpPro.ElasticSearch;

[DependsOn(typeof(AbpAutofacModule))]
public class AbpProElasticSearchModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpProElasticSearchOptions>(context.Services.GetConfiguration().GetSection("ElasticSearch"));
    }
}