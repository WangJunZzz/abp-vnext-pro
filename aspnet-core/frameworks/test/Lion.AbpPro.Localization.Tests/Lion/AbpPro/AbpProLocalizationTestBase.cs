using Volo.Abp;
using Volo.Abp.Testing;

namespace Lion.AbpPro
{

    public abstract class AbpProLocalizationTestBase : AbpIntegratedTest<AbpProLocalizationTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
