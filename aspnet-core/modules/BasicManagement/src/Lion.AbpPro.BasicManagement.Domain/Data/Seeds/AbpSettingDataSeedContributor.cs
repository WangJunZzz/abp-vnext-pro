using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Localization;

namespace Lion.AbpPro.BasicManagement.Data.Seeds
{
    public class AbpSettingDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private const string Value = "zh-Hans";
        private const string ProviderName = "G";
        private readonly ISettingManagementStore _settingManagementStore;

        public AbpSettingDataSeedContributor(ISettingManagementStore settingManagementStore)
        {
            _settingManagementStore = settingManagementStore;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _settingManagementStore.SetAsync(LocalizationSettingNames.DefaultLanguage, Value, ProviderName, null);
        }
    }
}