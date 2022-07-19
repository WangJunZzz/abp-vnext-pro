namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public DataDictionaryDataSeedContributor(
            IDataDictionaryRepository dataDictionaryRepository,
            IGuidGenerator guidGenerator, ICurrentTenant currentTenant)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                var gender = await _dataDictionaryRepository.FindByCodeAsync("Gender");
                if (gender == null)
                {
                    var id = context is {TenantId: null} ? DataDictionaryManagementConsts.SeedDataDictionaryId : _guidGenerator.Create();
                    var entity = new DataDictionary(id, "Gender", "性别", "单元测试", context?.TenantId);
                    entity.AddDetail(_guidGenerator.Create(), "Man", "男", 1, "单元测试");
                    entity.AddDetail(_guidGenerator.Create(), "WoMan", "女", 2, "单元测试");
                    entity.AddDetail(_guidGenerator.Create(), "None", "未知", 3, "单元测试", false);
                    await _dataDictionaryRepository.InsertAsync(entity);
                }
            }
        }
    }
}