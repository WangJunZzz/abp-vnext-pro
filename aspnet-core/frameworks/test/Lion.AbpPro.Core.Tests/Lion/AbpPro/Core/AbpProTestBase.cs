using Volo.Abp;
using Volo.Abp.Testing;

namespace Lion.AbpPro.Core
{
    public abstract class  AbpProTestBase : AbpIntegratedTest<AbpProTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}