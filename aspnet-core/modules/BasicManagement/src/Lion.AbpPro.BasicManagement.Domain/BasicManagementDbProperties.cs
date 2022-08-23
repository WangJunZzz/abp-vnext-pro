namespace Lion.AbpPro.BasicManagement;

public static class BasicManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "BasicManagement";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "BasicManagement";
}
