using Lion.AbpPro.NotificationManagement.Notifications.MaxLengths;

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
                b.Property(e => e.Title).IsRequired().HasMaxLength(NotificationMaxLengths.Length128);
                b.Property(e => e.Content).IsRequired().HasMaxLength(NotificationMaxLengths.Length1024);
                b.Property(e => e.SenderUserName).IsRequired().HasMaxLength(NotificationMaxLengths.Length128);
                b.Property(e => e.ReceiveUserName).HasMaxLength(NotificationMaxLengths.Length128);
                b.ConfigureByConvention();
            });

            builder.Entity<NotificationSubscription>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + "NotificationSubscriptions", NotificationManagementDbProperties.DbSchema);
                b.Property(e => e.ReceiveUserName).HasMaxLength(NotificationMaxLengths.Length128);
                b.HasIndex(e => e.NotificationId);
                b.HasIndex(e => e.ReceiveUserId);
                b.ConfigureByConvention();
            });
        }
    }
}