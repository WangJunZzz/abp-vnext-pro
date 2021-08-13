using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public class CommandNotificationAppService : NotificationManagementAppService, ICommandNotificationAppService
    {
        private readonly NotificationManager _notificationManager;

        public CommandNotificationAppService(NotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public Task SetReadAsync(SetReadInput input)
        {
            return _notificationManager.SetReadAsync(input.Id, input.ReceiveId);
        }
    }
}