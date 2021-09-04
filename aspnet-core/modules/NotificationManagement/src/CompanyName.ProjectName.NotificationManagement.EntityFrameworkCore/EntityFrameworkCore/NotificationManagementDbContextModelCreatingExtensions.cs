using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using CompanyName.ProjectName.NotificationManagement.Notifications;

namespace CompanyName.ProjectName.NotificationManagement.EntityFrameworkCore
{
    public static class NotificationManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureNotificationManagement(
            this ModelBuilder builder,
            Action<NotificationManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationManagementModelBuilderConfigurationOptions(
                NotificationManagementDbProperties.DbTablePrefix,
                NotificationManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Notification>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + nameof(Notification),
                    NotificationManagementDbProperties.DbSchema);
                b.ConfigureByConvention();
            });
            
            builder.Entity<NotificationSubscription>(b =>
            {
                b.ToTable(NotificationManagementDbProperties.DbTablePrefix + nameof(NotificationSubscription),
                    NotificationManagementDbProperties.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}