namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryManager_Tests : DataDictionaryManagementDomainTestBase
    {
        private readonly DataDictionaryManager _dataDictionaryManager;

        public DataDictionaryManager_Tests()
        {
            _dataDictionaryManager = GetRequiredService<DataDictionaryManager>();
        }

        [Fact]
        public async Task Test_FindByIdAsync_Ok()
        {
            var entity =
                await _dataDictionaryManager.FindByIdAsync(DataDictionaryManagementConsts.SeedDataDictionaryId);
            entity.DisplayText.ShouldBe("性别");
            entity.Details.Count.ShouldBe(3);
            entity.Details.FirstOrDefault(e => e.Code == "None").IsEnabled.ShouldBeFalse();

        
        }

        [Fact]
        public async Task Test_FindByCodeAsync_Ok()
        {
            var entity = await _dataDictionaryManager.FindByCodeAsync("Gender");
            entity.DisplayText.ShouldBe("性别");
            entity.Details.Count.ShouldBe(3);
         
        }

        [Fact]
        public async Task Test_CreateAsync_Ok()
        {
            var entity = await _dataDictionaryManager.CreateAsync("Xunit", "单元测试", "描述");
            entity.Code.ShouldBe("Xunit");
            var entity2 = await _dataDictionaryManager.CreateDetailAsync(entity.Id, "Detail", "明细", "测试", 1);
            entity2.Details.Count.ShouldBe(1);

            var exception = await Record.ExceptionAsync(async () =>
                await _dataDictionaryManager.CreateDetailAsync(Guid.NewGuid(), "Man", "明细", "测试", 1));
            exception.Message.ShouldBe($"数据字典不存在");

            var exception1 = await Record.ExceptionAsync(async () =>
                await _dataDictionaryManager.CreateDetailAsync(DataDictionaryManagementConsts.SeedDataDictionaryId,
                    "Man", "明细", "测试", 1));
            exception1.Message.ShouldBe($"字典项Man已存在");

        }
    }
}