namespace Lion.AbpPro.NotificationManagement.Hubs;

public interface INotificationHubAppService : IApplicationService
{
    Task SendMessageAsync(Guid id, string title, string content, MessageType messageType,MessageLevel messageLevel, List<string> users);
}