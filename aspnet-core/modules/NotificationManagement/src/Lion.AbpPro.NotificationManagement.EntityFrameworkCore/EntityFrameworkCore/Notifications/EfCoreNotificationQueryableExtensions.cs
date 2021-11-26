using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lion.AbpPro.NotificationManagement.Notifications;

namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore.Notifications
{
    public static class EfCoreNotificationQueryableExtensions
    {
        public static IQueryable<Notification> IncludeDetails(this IQueryable<Notification> queryable, bool include = true)
        {
            return !include ? queryable : queryable.Include(e => e.NotificationSubscriptions);
        }
    }
}