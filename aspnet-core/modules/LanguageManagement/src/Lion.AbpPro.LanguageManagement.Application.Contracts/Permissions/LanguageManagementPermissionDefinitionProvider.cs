namespace Lion.AbpPro.LanguageManagement.Permissions
{
    public class LanguageManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup("AbpIdentity");
            // var languageManagement = context.AddGroup(LanguageManagementPermissions.GroupName, L("Permission:LanguageManagement"));


            var languages = abpIdentityGroup.AddPermission(LanguageManagementPermissions.Languages.Default, L("Permission:Languages"));
            languages.AddChild(LanguageManagementPermissions.Languages.Create, L("Permission:Create"));
            languages.AddChild(LanguageManagementPermissions.Languages.Edit, L("Permission:Edit"));
            languages.AddChild(LanguageManagementPermissions.Languages.Delete, L("Permission:Delete"));
            languages.AddChild(LanguageManagementPermissions.Languages.ChangeDefault, L("Permission:LanguagesChangeDefault"));


            var languageTexts = abpIdentityGroup.AddPermission(LanguageManagementPermissions.LanguageTexts.Default, L("Permission:LanguageTexts"));
            languageTexts.AddChild(LanguageManagementPermissions.LanguageTexts.Edit, L("Permission:Edit"));
            languageTexts.AddChild(LanguageManagementPermissions.LanguageTexts.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LanguageManagementResource>(name);
        }
    }
}