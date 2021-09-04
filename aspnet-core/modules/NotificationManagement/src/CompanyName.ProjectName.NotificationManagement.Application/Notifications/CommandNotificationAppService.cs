using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Volo.Abp.Users;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public class CommandNotificationAppService : NotificationManagementAppService, ICommandNotificationAppService
    {
        private readonly NotificationManager _notificationManager;

        private readonly ICurrentUser _currentUser;
        public CommandNotificationAppService(NotificationManager notificationManager, ICurrentUser currentUser)
        {
            _notificationManager = notificationManager;
            _currentUser = currentUser;
        }

        public Task SetReadAsync(SetReadInput input)
        {
            return _notificationManager.SetReadAsync(input.Id, input.ReceiveId);
        }
        
        public async Task CreateAsync(CreateNotificationInput input)
        {
            if (_currentUser.Id != null)
                await _notificationManager.CreateAsync(input.Title, input.Content, _currentUser.Id.Value, input.ReceiveIds,
                    input.MessageType);
        }
    }
}