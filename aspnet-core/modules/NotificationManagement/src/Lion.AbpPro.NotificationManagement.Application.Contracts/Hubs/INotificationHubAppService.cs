using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lion.AbpPro.NotificationManagement.Notifications.Enums;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.NotificationManagement.Hubs;

public interface INotificationHubAppService : IApplicationService
{
    Task SendMessageAsync(Guid id, string title, string content, MessageType messageType,MessageLevel messageLevel, List<string> users);
}