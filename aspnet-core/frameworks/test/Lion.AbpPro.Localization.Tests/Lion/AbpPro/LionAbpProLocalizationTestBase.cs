using Volo.Abp;
using Volo.Abp.Testing;

namespace Lion.AbpPro
{

    public abstract class LionAbpProLocalizationTestBase : AbpIntegratedTest<LionAbpProLocalizationTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
