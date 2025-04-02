using Volo.Abp.Identity.Settings;

namespace Lion.AbpPro.BasicManagement.Settings;

public class BasicManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpProSettingConsts.MySetting1));
        OverrideDefaultSettings(context);
    }

    /// <summary>
    /// 重写默认setting添加自定义属性
    /// </summary>
    private static void OverrideDefaultSettings(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(TimingSettingNames.TimeZone,
                    "China Standard Time",
                    L("DisplayName:Abp.Timing.Timezone"),
                    L("Description:Abp.Timing.Timezone"))
                .WithProperty(BasicManagementSettings.Group.Default,
                    BasicManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default,
                    AbpProSettingConsts.ControlType.TypeText));

        context.GetOrNull(IdentitySettingNames.Password.RequiredLength)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);

        context.GetOrNull(IdentitySettingNames.Password.RequiredUniqueChars)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);

        context.GetOrNull(IdentitySettingNames.Password.RequireNonAlphanumeric)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull(IdentitySettingNames.Password.RequireLowercase)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull(IdentitySettingNames.Password.RequireUppercase)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull(IdentitySettingNames.Password.RequireDigit)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);
        
        context.GetOrNull(IdentitySettingNames.Lockout.LockoutDuration)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);
        
        context.GetOrNull(IdentitySettingNames.Lockout.MaxFailedAccessAttempts)
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);
        
        context.Add(
            new SettingDefinition(BasicManagementSettings.EnableNewAccountRequiredChangePassword,
                    "false",
                    L(BasicManagementConsts.EnableNewAccountRequiredChangePassword),
                    L($"Description:{BasicManagementConsts.EnableNewAccountRequiredChangePassword}"))
                .WithProperty(BasicManagementSettings.Group.Default,
                    BasicManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default,
                    AbpProSettingConsts.ControlType.TypeCheckBox));
        context.Add(
            new SettingDefinition(BasicManagementSettings.EnableExpireRequiredChangePassword,
                    "false",
                    L(BasicManagementConsts.EnableExpireRequiredChangePassword),
                    L($"Description:{BasicManagementConsts.EnableExpireRequiredChangePassword}"))
                .WithProperty(BasicManagementSettings.Group.Default,
                    BasicManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default,
                    AbpProSettingConsts.ControlType.TypeCheckBox));
        
        context.Add(
            new SettingDefinition(BasicManagementSettings.PasswordExpireDay,
                    "90",
                    L(BasicManagementConsts.PasswordExpireDay),
                    L($"Description:{BasicManagementConsts.PasswordExpireDay}"))
                .WithProperty(BasicManagementSettings.Group.Default,
                    BasicManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default,
                    AbpProSettingConsts.ControlType.Number));
        
        context.Add(
            new SettingDefinition(BasicManagementSettings.PasswordRemindDay,
                    "7",
                    L(BasicManagementConsts.PasswordRemindDay),
                    L($"Description:{BasicManagementConsts.PasswordRemindDay}"))
                .WithProperty(BasicManagementSettings.Group.Default,
                    BasicManagementSettings.Group.SystemManagement)
                .WithProperty(AbpProSettingConsts.ControlType.Default,
                    AbpProSettingConsts.ControlType.Number));
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BasicManagementResource>(name);
    }
}