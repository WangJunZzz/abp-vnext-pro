namespace Lion.AbpPro.LanguageManagement.Settings
{
    public class LanguageManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var languageManagementSettings = context.GetOrNull(LocalizationSettingNames.DefaultLanguage)
                .WithProperty(LanguageManagementSettings.Group.Default, LanguageManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default, AbpProSettingConsts.ControlType.TypeText);
            languageManagementSettings.DefaultValue = "zh-Hans";
        }
    }
}