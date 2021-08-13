using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    public class DataDictionaryManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public DataDictionaryManagementDataSeedContributor(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IDataDictionaryRepository dataDictionaryRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            using (_currentTenant.Change(context?.TenantId))
            {
                // var id = _guidGenerator.Create();
                // var entity = new DataDictionary(id, "Gender", "性别", "单元测试", context?.TenantId);
                // entity.AddDetail(_guidGenerator.Create(), "Man", "男", 1, "测试", true);
                // entity.AddDetail(_guidGenerator.Create(), "WoMan", "女", 2, "测试", true);
                // entity.AddDetail(_guidGenerator.Create(), "None", "未知", 3, "测试", false);
                // await _dataDictionaryRepository.InsertAsync(entity);
            }
        }
    }
}