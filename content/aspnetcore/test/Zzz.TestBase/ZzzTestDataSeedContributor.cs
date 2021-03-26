using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Zzz.Dic;
using Zzz.Users;

namespace Zzz
{
    public class ZzzTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<DataDictionary> _dataDictionaryRepository;

        public ZzzTestDataSeedContributor(
            IRepository<DataDictionary> dataDictionaryRepository)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            await InitDicDataAsync();
        }


        private async Task InitDicDataAsync()
        {
            var dic = new DataDictionary(Guid.NewGuid(), "Group", null, "单元测试");
            dic.AddDataDictionaryDetail(new DataDictionaryDetail(Guid.NewGuid(), "Test", "007", 1));
            await _dataDictionaryRepository.InsertAsync(dic);
        }
    }
}