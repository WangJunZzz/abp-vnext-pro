using Lion.AbpPro.FreeSqlReppsitory.Tests;
using Lion.AbpPro.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion.AbpPro
{
    public abstract class AbpProFreeSqlRepositoryTestBase: AbpProTestBase<AbpProFreeSqlRepositoryTestModule>
    {
        public AbpProFreeSqlRepositoryTestBase()
        {
            ServiceProvider.InitializeLocalization();
        }
    }
}
