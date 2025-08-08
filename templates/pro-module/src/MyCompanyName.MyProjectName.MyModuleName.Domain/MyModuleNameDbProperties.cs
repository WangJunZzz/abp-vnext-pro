namespace MyCompanyName.MyProjectName.MyModuleName
{
    public static class MyModuleNameDbProperties
    {
        public static string DbTablePrefix { get; set; } = "AbpPro";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "MyModuleName";
    }
}
