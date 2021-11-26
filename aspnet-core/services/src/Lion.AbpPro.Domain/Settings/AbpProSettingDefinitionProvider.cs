using Lion.AbpPro.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Lion.AbpPro.Settings
{
    public class AbpProSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AbpProSettings.MySetting1));
            OverrideDefalutSettings(context);
        }

        /// <summary>
        /// 重写默认setting添加自定义属性
        /// </summary>
        private static void OverrideDefalutSettings(ISettingDefinitionContext context)
        {
            context.GetOrNull("Abp.Localization.DefaultLanguage")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeText);

            context.GetOrNull("Abp.Identity.Password.RequiredLength")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequiredUniqueChars")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireNonAlphanumeric")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireLowercase")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireUppercase")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireDigit")
                .WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.Add(new SettingDefinition(
                    AbpProSettings.Other.Github,
                    "https://github.com/WangJunZzz/abp-vnext-pro",
                    L("DisplayName:" + AbpProSettings.Other.Github),
                    L("Description:" + AbpProSettings.Other.Github)
                ).WithProperty(AbpProSettings.Group.Defalut,
                    AbpProSettings.Group.OtherManagement)
                .WithProperty(AbpProSettings.ControlType.Defalut,
                    AbpProSettings.ControlType.TypeText));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}