using CompanyName.ProjectName.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace CompanyName.ProjectName.Settings
{
    public class ProjectNameSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(ProjectNameSettings.MySetting1));
            Test(context);
        }

        /// <summary>
        /// 测试Setting
        /// </summary>
        /// <param name="context"></param>
        public void Test(ISettingDefinitionContext context)
        {
            // 具体设置请参考  https://github.com/EasyAbp/Abp.SettingUi/blob/develop/docs/README_zh-Hans.md
            context.Add(
                new SettingDefinition(
                    name: "Test",
                    defaultValue: "Test Setting Module",
                    displayName: L("Test")
                ).WithProviders( // 设置级别
                    DefaultValueSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectNameResource>(name);
        }
    }
}