using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DataDictionaryManagement.EntityFrameworkCore.DataDictionaries
{
    public abstract class
        EfCoreDataDictionaryRepository_Tests<TStartupModule> : DataDictionaryManagementTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        protected EfCoreDataDictionaryRepository_Tests()
        {
            _dataDictionaryRepository = GetRequiredService<IDataDictionaryRepository>();
        }

    }
}