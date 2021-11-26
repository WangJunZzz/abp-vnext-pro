using System.Threading.Tasks;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.DataDictionaries
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