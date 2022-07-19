namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryAppService_Tests : DataDictionaryManagementApplicationTestBase
    {
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public DataDictionaryAppService_Tests()
        {
            _dataDictionaryAppService = GetRequiredService<IDataDictionaryAppService>();
        }

        [Fact]
        public async Task Test_GetPagingListAsync_Ok()
        {
            var result = await _dataDictionaryAppService.GetPagingListAsync(new PagingDataDictionaryInput()
            {
                Filter = "Gender"
            });

            result.TotalCount.ShouldBe(1);
            result.Items.FirstOrDefault().DisplayText.ShouldBe("性别");
            var result1 = await _dataDictionaryAppService.GetPagingListAsync(new PagingDataDictionaryInput()
            {
                Filter = "性别"
            });

            result1.TotalCount.ShouldBe(1);
            result1.Items.FirstOrDefault().Code.ShouldBe("Gender");
        }

        [Fact]
        public async Task Test_CreateAsync_Ok()
        {
            var input = new CreateDataDictinaryInput()
            {
                Code = "Xunit",
                DisplayText = "单元测试"
            };
            await _dataDictionaryAppService.CreateAsync(input);

            var result = await _dataDictionaryAppService.GetPagingListAsync(new PagingDataDictionaryInput());
            result.TotalCount.ShouldBe(2);
        }

        [Fact]
        public async Task Test_CreateDetailAsync_Ok()
        {
            var input = new CreateDataDictinaryDetailInput()
            {
                Id = DataDictionaryManagementConsts.SeedDataDictionaryId,
                Code = "Detail",
                DisplayText = "明细",
                Description = "单元测试",
            };

            await _dataDictionaryAppService.CreateDetailAsync(input);

            var result = await _dataDictionaryAppService.GetPagingDetailListAsync(
                new PagingDataDictionaryDetailInput()
                {
                    DataDictionaryId = DataDictionaryManagementConsts.SeedDataDictionaryId,
                    Filter = "Detail"
                }
            );

            result.Items.Any(e => e.Code == "Detail" && e.IsEnabled == true).ShouldBeTrue();
        }
    }
}