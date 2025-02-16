namespace Lion.AbpPro.FileManagement.Permissions;

public class FileManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpIdentityGroup = context.GetGroup("AbpIdentity");

        var fileManagement = abpIdentityGroup.AddPermission(FileManagementPermissions.FileManagement.Default,
            L("Permission:FileManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileManagementResource>(name);
    }
}