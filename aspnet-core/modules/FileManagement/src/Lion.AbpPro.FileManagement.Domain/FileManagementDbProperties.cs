namespace Lion.AbpPro.FileManagement;

public static class FileManagementDbProperties
{
    public const string ConnectionStringName = "FileManagement";
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;
}