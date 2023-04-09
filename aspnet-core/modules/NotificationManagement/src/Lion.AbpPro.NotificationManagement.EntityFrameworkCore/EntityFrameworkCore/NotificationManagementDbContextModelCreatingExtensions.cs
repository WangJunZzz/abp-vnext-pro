namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore
{
    public static class NotificationManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureNotificationManagement(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Notification>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + "Notifications", NotificationManagementDbProperties.DbSchema);
                b.HasMany(e => e.NotificationSubscriptions).WithOne().HasForeignKey(uc => uc.NotificationId).IsRequired();
                b.ConfigureByConvention();
            });

            builder.Entity<NotificationSubscription>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + "NotificationSubscriptions", NotificationManagementDbProperties.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}