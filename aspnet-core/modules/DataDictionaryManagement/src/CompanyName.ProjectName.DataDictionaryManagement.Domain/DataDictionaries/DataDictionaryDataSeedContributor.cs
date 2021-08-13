using System;
using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IGuidGenerator _guidGenerator;

        public DataDictionaryDataSeedContributor(
            IDataDictionaryRepository dataDictionaryRepository,
            IGuidGenerator guidGenerator)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var gender = await _dataDictionaryRepository.FindByCodeAsync("Gender");
            if (gender == null)
            {
                var id = DataDictionaryManagementConsts.SeedDataDictionaryId;//_guidGenerator.Create();
                var entity = new DataDictionary(id, "Gender", "性别", "单元测试");
                entity.AddDetail(_guidGenerator.Create(), "Man", "男", 1, "单元测试");
                entity.AddDetail(_guidGenerator.Create(), "WoMan", "女", 2, "单元测试");
                entity.AddDetail(_guidGenerator.Create(), "None", "未知", 3, "单元测试", false);
                await _dataDictionaryRepository.InsertAsync(entity);
            }
        }
    }
}