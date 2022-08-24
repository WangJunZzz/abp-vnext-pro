using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.BasicManagement.Data.Seeds
{
    public class AbpSettingDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ISettingManager _settingManager;
        private const string DefaultLanguageKey = "Abp.Localization.DefaultLanguage";
        private const string DefaultLanguage = "zh-Hans";
        public AbpSettingDataSeedContributor(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // 设置默认语言
            await _settingManager.SetGlobalAsync(DefaultLanguageKey, DefaultLanguage);
        }
    }
}