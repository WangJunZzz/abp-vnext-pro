namespace Lion.AbpPro.NotificationManagement
{
    public static class NotificationManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "AbpPro";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "NotificationManagement";
    }
}
