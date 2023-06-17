using Volo.Abp;
using Volo.Abp.Testing;

namespace Lion.AbpPro.Core.Cli
{
    public abstract class  LionAbpProCoreCliTestBase : AbpIntegratedTest<LionAbpProCoreCliTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}