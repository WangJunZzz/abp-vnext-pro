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

        context.GetOrNull("Abp.Identity.Password.RequiredLength")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);

        context.GetOrNull("Abp.Identity.Password.RequiredLength")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);

        context.GetOrNull("Abp.Identity.Password.RequiredUniqueChars")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.Number);

        context.GetOrNull("Abp.Identity.Password.RequireNonAlphanumeric")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull("Abp.Identity.Password.RequireLowercase")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull("Abp.Identity.Password.RequireUppercase")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);

        context.GetOrNull("Abp.Identity.Password.RequireDigit")
            .WithProperty(BasicManagementSettings.Group.Default,
                BasicManagementSettings.Group.SystemManagement)
            .WithProperty(AbpProSettingConsts.ControlType.Default,
                AbpProSettingConsts.ControlType.TypeCheckBox);
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BasicManagementResource>(name);
    }
}