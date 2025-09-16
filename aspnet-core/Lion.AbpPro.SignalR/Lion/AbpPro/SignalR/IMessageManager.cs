namespace Lion.AbpPro.SignalR;

public interface IMessageManager
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="title">消息标题</param>
    /// <param name="content">消息内容</param>
    /// <param name="messageType">消息类型</param>
    /// <param name="messageLevel">消息级别</param>
    /// <param name="receiverUserId">消息接受人，如果是广播消息，不需要传递</param>
    /// <param name="receiverUserName">消息接受人userName，如果是广播消息，不需要传递</param>
    /// <param name="isPersistent">是否持久化,如果ture会在消息管理中出现,并且右上角也会存在</param>
    /// <returns></returns>
    Task SendMessageAsync(string title, string content, MessageType messageType, MessageLevel messageLevel, Guid? receiverUserId = null, string receiverUserName = "", bool isPersistent = true);
}