namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore
{
    public class NotificationManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NotificationManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}