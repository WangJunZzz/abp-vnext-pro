using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Hubs;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp;
using Volo.Abp.Users;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public class NotificationAppService : NotificationManagementAppService, INotificationAppService
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
        private readonly NotificationManager _notificationManager;
        private readonly ICurrentUser _currentUser;

        public NotificationAppService(
            IHubContext<NotificationHub, INotificationHub> hubContext, NotificationManager notificationManager,
            ICurrentUser currentUser)
        {
            _hubContext = hubContext;
            _notificationManager = notificationManager;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public async Task SendMessageAsync(string title, string content, MessageType messageType, List<string> users)
        {
            switch (messageType)
            {
                case MessageType.Text:
                    await SendMessageToClientByUserIdAsync(new SendNotificationDto(title, content, messageType), users);
                    break;
                case MessageType.BroadCast:
                    await SendMessageToAllClientAsync(new SendNotificationDto(title, content, messageType));
                    break;
                default:
                    throw new UserFriendlyException("未知的消息类型");
            }
        }


        /// <summary>
        /// 发送消息指定客户端用户
        /// </summary>
        /// <param name="sendNotificationDto"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task SendMessageToClientByUserIdAsync(SendNotificationDto sendNotificationDto,
            List<string> users)
        {
            if (users is {Count: > 0})
            {
                await _hubContext.Clients
                    .Users(users.AsReadOnly().ToList())
                    .ReceiveTextMessageAsync(sendNotificationDto);
            }
        }

        /// <summary>
        /// 发送消息到所有客户端
        /// 广播消息
        /// </summary>
        /// <param name="sendNotificationDto"></param>
        private async Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto)
        {
            await _hubContext.Clients.All.ReceiveBroadCastMessageAsync(sendNotificationDto);
        }
        
    }
}