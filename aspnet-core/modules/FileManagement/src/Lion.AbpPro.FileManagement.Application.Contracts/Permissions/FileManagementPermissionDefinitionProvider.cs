namespace Lion.AbpPro.FileManagement.Permissions;

public class FileManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var fileManagementGroup = context.AddGroup(FileManagementPermissions.GroupName, L("Permission:FileManagement"));
        var filePermission = fileManagementGroup.AddPermission(FileManagementPermissions.FileManagement.Default, L("Permission:FileManagement:File"));
        filePermission.AddChild(FileManagementPermissions.FileManagement.Upload, L("Permission:FileManagement:File:Upload"));
        filePermission.AddChild(FileManagementPermissions.FileManagement.Download, L("Permission:FileManagement:File:Download"));
        filePermission.AddChild(FileManagementPermissions.FileManagement.Delete, L("Permission:FileManagement:File:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileManagementResource>(name);
    }
}