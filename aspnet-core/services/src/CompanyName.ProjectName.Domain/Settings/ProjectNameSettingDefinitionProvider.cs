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
            OverrideDefalutSettings(context);
        }

        /// <summary>
        /// 重写默认setting添加自定义属性
        /// </summary>
        private static void OverrideDefalutSettings(ISettingDefinitionContext context)
        {
            context.GetOrNull("Abp.Localization.DefaultLanguage")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeText);

            context.GetOrNull("Abp.Identity.Password.RequiredLength")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequiredUniqueChars")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireNonAlphanumeric")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireLowercase")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireUppercase")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.GetOrNull("Abp.Identity.Password.RequireDigit")
                .WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.SystemManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeCheckBox);

            context.Add(new SettingDefinition(
                    ProjectNameSettings.Other.Github,
                    "https://github.com/WangJunZzz/abp-vnext-pro",
                    L("DisplayName:" + ProjectNameSettings.Other.Github),
                    L("Description:" + ProjectNameSettings.Other.Github)
                ).WithProperty(ProjectNameSettings.Group.Defalut,
                    ProjectNameSettings.Group.OtherManagement)
                .WithProperty(ProjectNameSettings.ControlType.Defalut,
                    ProjectNameSettings.ControlType.TypeText));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectNameResource>(name);
        }
    }
}