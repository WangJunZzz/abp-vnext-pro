namespace Lion.AbpPro.LanguageManagement
{
    public static class LanguageManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "AbpPro";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "LanguageManagement";
    }
}
