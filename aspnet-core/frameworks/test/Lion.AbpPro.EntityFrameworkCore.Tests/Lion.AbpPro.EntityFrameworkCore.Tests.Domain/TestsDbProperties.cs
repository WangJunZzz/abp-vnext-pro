namespace Lion.AbpPro.EntityFrameworkCore.Tests;

public static class TestsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Tests";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Tests";
}
