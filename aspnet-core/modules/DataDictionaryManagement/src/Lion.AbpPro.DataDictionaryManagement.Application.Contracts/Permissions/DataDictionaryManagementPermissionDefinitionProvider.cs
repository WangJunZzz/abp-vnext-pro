namespace Lion.AbpPro.DataDictionaryManagement.Permissions
{
    public class DataDictionaryManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup("AbpIdentity");

            var dataDictionaryManagement = abpIdentityGroup.AddPermission(DataDictionaryManagementPermissions.DataDictionaryManagement.Default,
                L("Permission:DataDictionaryManagement"));
            dataDictionaryManagement.AddChild(DataDictionaryManagementPermissions.DataDictionaryManagement.Create, L("Permission:Create"));
            dataDictionaryManagement.AddChild(DataDictionaryManagementPermissions.DataDictionaryManagement.Update, L("Permission:Update"));
            dataDictionaryManagement.AddChild(DataDictionaryManagementPermissions.DataDictionaryManagement.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DataDictionaryManagementResource>(name);
        }
    }
}