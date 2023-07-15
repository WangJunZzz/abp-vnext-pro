namespace Lion.AbpPro.LanguageManagement.Permissions
{
    public class LanguageManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup("AbpIdentity");
            // var languageManagement = context.AddGroup(LanguageManagementPermissions.GroupName, L("Permission:LanguageManagement"));


            var languages = abpIdentityGroup.AddPermission(LanguageManagementPermissions.Languages.Default, L("Permission:Languages"), multiTenancySide: MultiTenancySides.Both);
            languages.AddChild(LanguageManagementPermissions.Languages.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Both);
            languages.AddChild(LanguageManagementPermissions.Languages.Update, L("Permission:Update"), multiTenancySide: MultiTenancySides.Both);
            languages.AddChild(LanguageManagementPermissions.Languages.Delete, L("Permission:Delete"), multiTenancySide: MultiTenancySides.Both);
            languages.AddChild(LanguageManagementPermissions.Languages.ChangeDefault, L("Permission:LanguagesChangeDefault"), multiTenancySide: MultiTenancySides.Both);


            var languageTexts = abpIdentityGroup.AddPermission(LanguageManagementPermissions.LanguageTexts.Default, L("Permission:LanguageTexts"), multiTenancySide: MultiTenancySides.Both);
            languageTexts.AddChild(LanguageManagementPermissions.LanguageTexts.Update, L("Permission:Update"), multiTenancySide: MultiTenancySides.Both);
            languageTexts.AddChild(LanguageManagementPermissions.LanguageTexts.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Both);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LanguageManagementResource>(name);
        }
    }
}