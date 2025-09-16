namespace Lion.AbpPro.SignalR.Hubs
{
    public interface INotificationHub
    {
        /// <summary>
        /// 接受普通消息
        /// </summary>
        Task ReceiveTextMessageAsync(SendNotificationDto message);

        /// <summary>
        /// 接受广播消息
        /// </summary>
        Task ReceiveBroadCastMessageAsync(SendNotificationDto message);
    }
}