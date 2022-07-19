namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.DataDictionaries
{
    public class EfCoreDataDictionaryRepository_Tests : EfCoreDataDictionaryRepository_Tests<
        DataDictionaryManagementEntityFrameworkCoreTestModule>
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public EfCoreDataDictionaryRepository_Tests()
        {
            _dataDictionaryRepository = GetRequiredService<IDataDictionaryRepository>();
        }

        [Fact]
        public async Task Test_FindByIdAsync_Ok()
        {
            var entity =
                await _dataDictionaryRepository.FindByIdAsync(DataDictionaryManagementConsts.SeedDataDictionaryId,
                    true);
            entity.DisplayText.ShouldBe("性别");
            entity.Details.Count.ShouldBe(3);
            entity.Details.FirstOrDefault(e => e.Code == "None").IsEnabled.ShouldBeFalse();

            var noDetailEntity =
                await _dataDictionaryRepository.FindByIdAsync(DataDictionaryManagementConsts.SeedDataDictionaryId,
                    false);
            noDetailEntity.Details.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Test_FindByCodeAsync_Ok()
        {
            var entity = await _dataDictionaryRepository.FindByCodeAsync("Gender", true);
            entity.DisplayText.ShouldBe("性别");
            entity.Details.Count.ShouldBe(3);
            entity.Details.FirstOrDefault(e => e.Code == "None").IsEnabled.ShouldBeFalse();
            var noDetailEntity = await _dataDictionaryRepository.FindByCodeAsync("Gender", false);
            noDetailEntity.Details.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Test_FindByDisplayTextAsync_Ok()
        {
            var entity = await _dataDictionaryRepository.FindByDisplayTextAsync("性别", true);
            entity.Code.ShouldBe("Gender");
            entity.Details.Count.ShouldBe(3);
            entity.Details.FirstOrDefault(e => e.Code == "None").IsEnabled.ShouldBeFalse();
            var noDetailEntity = await _dataDictionaryRepository.FindByDisplayTextAsync("性别", false);
            noDetailEntity.Details.Count.ShouldBe(0);
        }
        
        [Fact]
        public async Task Test_GetPagingListAsync_Ok()
        {
            var list = await _dataDictionaryRepository.GetPagingListAsync("性别");
            list.Count.ShouldBe(1);
            var list2 = await _dataDictionaryRepository.GetPagingListAsync("Gender");
            list2.Count.ShouldBe(1);
            var list3 = await _dataDictionaryRepository.GetPagingCountAsync("性别");
            list.Count.ShouldBe(1);
            var list4 = await _dataDictionaryRepository.GetPagingCountAsync("Gender");
            list2.Count.ShouldBe(1);
            list.Count.ShouldBe(Convert.ToInt32(list3));
            list2.Count.ShouldBe(Convert.ToInt32(list4));
        }
    }
}