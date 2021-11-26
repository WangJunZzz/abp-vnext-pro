namespace Lion.AbpPro.DataDictionaryManagement
{
    public static class DataDictionaryManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "DataDictionaryManagement";
    }
}
