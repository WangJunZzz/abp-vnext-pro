using Volo.Abp.Testing;

namespace Lion.AbpPro.ElasticSearch
{

    public abstract class AbpProElasticSearchTestBase : AbpIntegratedTest<AbpProElasticSearchTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
