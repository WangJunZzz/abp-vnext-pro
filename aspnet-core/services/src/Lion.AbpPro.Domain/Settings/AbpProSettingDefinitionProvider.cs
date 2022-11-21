namespace Lion.AbpPro.Settings
{
    public class AbpProSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            ConfigEmail(context);
        }

       private static void ConfigEmail(ISettingDefinitionContext context)
        {
            context.GetOrNull(EmailSettingNames.Smtp.Host)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);

            context.GetOrNull(EmailSettingNames.Smtp.Port)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.Number);

            context.GetOrNull(EmailSettingNames.Smtp.UserName)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);

            context.GetOrNull(EmailSettingNames.Smtp.Password)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);
            

            context.GetOrNull(EmailSettingNames.Smtp.EnableSsl)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull(EmailSettingNames.Smtp.UseDefaultCredentials)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeCheckBox);

            context.GetOrNull(EmailSettingNames.DefaultFromAddress)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);
            
            context.GetOrNull(EmailSettingNames.DefaultFromDisplayName)
                .WithProperty(AbpProSettings.Group.Default,
                    AbpProSettings.Group.EmailManagement)
                .WithProperty(AbpProSettings.ControlType.Default,
                    AbpProSettings.ControlType.TypeText);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}