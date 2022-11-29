using Volo.Abp;
using Volo.Abp.Testing;

namespace Lion.AbpPro.Core
{
    public abstract class  LionAbpProTestBase : AbpIntegratedTest<LionAbpProTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}