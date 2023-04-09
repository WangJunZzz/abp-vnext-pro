using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Lion.AbpPro.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpProEntityFrameworkCoreModule),
        typeof(AbpProApplicationContractsModule)
        )]
    public class AbpProDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
            ConfigureLocalization();
        }
        
        /// <summary>
        /// 多语言配置
        /// </summary>
        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English", "de"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "Hans"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文", "Hant"));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.ServiceProvider.GetRequiredService<ISettingDefinitionManager>().Get(LocalizationSettingNames.DefaultLanguage).DefaultValue = "zh-Hans";  
        }
    }
}
