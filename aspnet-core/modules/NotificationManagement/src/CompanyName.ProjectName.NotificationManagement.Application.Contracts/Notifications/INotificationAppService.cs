using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public interface INotificationAppService:IApplicationService
    {
        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <returns></returns>
        Task SendMessageAsync(string title, string content, MessageType messageType, List<string> users);
        
        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SetReadAsync(SetReadInput input);
        
        /// <summary>
        /// 创建一个消息
        /// 测试使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateNotificationInput input);
        
        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingNotificationListOutput>> GetPageTextNotificationByUserIdAsync(
            PagingNotificationListInput listInput);

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingNotificationListOutput>> GetPageBroadCastNotificationByUserIdAsync(
            PagingNotificationListInput listInput);
    }
}