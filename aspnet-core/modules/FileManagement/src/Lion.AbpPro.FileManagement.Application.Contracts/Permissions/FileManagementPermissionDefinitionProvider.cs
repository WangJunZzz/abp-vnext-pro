namespace Lion.AbpPro.FileManagement.Permissions;

public class FileManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
      
        var abpIdentityGroup = context.GetGroup("AbpIdentity");

        var dataDictionaryManagement = abpIdentityGroup.AddPermission(FileManagementPermissions.FileManagement.Default,
            L("Permission:FileManagement"));
        dataDictionaryManagement.AddChild(FileManagementPermissions.FileManagement.Upload, L("Permission:Upload"));
        //dataDictionaryManagement.AddChild(FileManagementPermissions.FileManagement.Down, L("Permission:Down"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileManagementResource>(name);
    }
}