using Lion.AbpPro.Localization;

namespace Lion.AbpPro
{
    public abstract class AbpProDomainTestBase : AbpProTestBase<AbpProDomainTestModule> 
    {
        public AbpProDomainTestBase()
        {
            ServiceProvider.InitializeLocalization();;
        }
    }
}
