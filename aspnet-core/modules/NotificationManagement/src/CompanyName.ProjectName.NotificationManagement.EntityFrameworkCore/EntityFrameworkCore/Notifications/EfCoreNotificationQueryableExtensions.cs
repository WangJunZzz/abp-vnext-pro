using System.Linq;
using Microsoft.EntityFrameworkCore;
using CompanyName.ProjectName.NotificationManagement.Notifications;

namespace CompanyName.ProjectName.NotificationManagement.EntityFrameworkCore.Notifications
{
    public static class EfCoreNotificationQueryableExtensions
    {
        public static IQueryable<Notification> IncludeDetails(this IQueryable<Notification> queryable, bool include = true)
        {
            return !include ? queryable : queryable.Include(e => e.NotificationSubscriptions);
        }
    }
}