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
                
                // 租户id
                b.Property(e => e.TenantId)
                    .HasComment("租户id");
                
                // 消息标题 - 必填，最大长度128
                b.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(NotificationMaxLengths.Length128)
                    .HasComment("消息标题");
                
                // 消息内容 - 必填，最大长度1024
                b.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(NotificationMaxLengths.Length1024)
                    .HasComment("消息内容");
                
                // 消息类型
                b.Property(e => e.MessageType)
                    .HasComment("消息类型");
                
                // 消息等级
                b.Property(e => e.MessageLevel)
                    .HasComment("消息等级");
                
                // 发送人用户Id - 必填
                b.Property(e => e.SenderUserId)
                    .HasComment("发送人用户Id");
                
                // 发送人用户名 - 必填，最大长度128
                b.Property(e => e.SenderUserName)
                    .IsRequired()
                    .HasMaxLength(NotificationMaxLengths.Length128)
                    .HasComment("发送人用户名");
                
                // 接收人用户Id
                b.Property(e => e.ReceiveUserId)
                    .HasComment("接收人用户Id");
                
                // 接收人用户名 - 最大长度128
                b.Property(e => e.ReceiveUserName)
                    .HasMaxLength(NotificationMaxLengths.Length128)
                    .HasComment("接收人用户名");
                
                // 是否已读 - 必填
                b.Property(e => e.Read)
                    .IsRequired()
                    .HasComment("是否已读");
                
                // 已读时间
                b.Property(e => e.ReadTime)
                    .HasComment("已读时间");
                
                b.ConfigureByConvention();
            });

            builder.Entity<NotificationSubscription>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + "NotificationSubscriptions", NotificationManagementDbProperties.DbSchema);
                
                // 租户id
                b.Property(e => e.TenantId)
                    .HasComment("租户id");
                
                // 消息Id - 必填
                b.Property(e => e.NotificationId)
                    .IsRequired()
                    .HasComment("消息Id");
                
                // 接收人用户Id - 必填
                b.Property(e => e.ReceiveUserId)
                    .IsRequired()
                    .HasComment("接收人用户Id");
                
                // 接收人用户名 - 最大长度128
                b.Property(e => e.ReceiveUserName)
                    .HasMaxLength(NotificationMaxLengths.Length128)
                    .HasComment("接收人用户名");
                
                // 是否已读 - 必填
                b.Property(e => e.Read)
                    .IsRequired()
                    .HasComment("是否已读");
                
                // 已读时间 - 必填
                b.Property(e => e.ReadTime)
                    .IsRequired()
                    .HasComment("已读时间");
                
                b.HasIndex(e => e.NotificationId);
                b.HasIndex(e => e.ReceiveUserId);
                b.ConfigureByConvention();
            });
        }
    }
}