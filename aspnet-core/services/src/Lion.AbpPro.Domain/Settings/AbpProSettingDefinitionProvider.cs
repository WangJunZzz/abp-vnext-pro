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
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);

            context.GetOrNull("Abp.Identity.Password.RequiredLength")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.Number);

            context.GetOrNull("Abp.Identity.Password.RequiredUniqueChars")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.Number);

            context.GetOrNull("Abp.Identity.Password.RequireNonAlphanumeric")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireLowercase")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireUppercase")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireDigit")
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.SystemManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.Add(new SettingDefinition(
                    AbpProSettings.Other.Github,
                    "https://github.com/WangJunZzz/abp-vnext-pro",
                    L("DisplayName:" + AbpProSettings.Other.Github),
                    L("Description:" + AbpProSettings.Other.Github)
                ).WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.OtherManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}