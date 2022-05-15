using Lion.AbpPro.FreeSqlReppsitory.Tests;
using Lion.AbpPro.Localization;

namespace Lion.AbpPro.FreeSqlRepository.Tests
{
    public abstract class AbpProFreeSqlRepositoryTestBase: AbpProTestBase<AbpProFreeSqlRepositoryTestModule>
    {
        protected AbpProFreeSqlRepositoryTestBase()
        {
            ServiceProvider.InitializeLocalization();
        }
    }
}
