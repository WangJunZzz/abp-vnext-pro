using MyCompanyName.MyProjectName.FreeSqlReppsitory.Tests;
using MyCompanyName.MyProjectName.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.MyProjectName
{
    public abstract class MyProjectNameFreeSqlRepositoryTestBase: MyProjectNameTestBase<MyProjectNameFreeSqlRepositoryTestModule>
    {
        public MyProjectNameFreeSqlRepositoryTestBase()
        {
            ServiceProvider.InitializeLocalization();
        }
    }
}
