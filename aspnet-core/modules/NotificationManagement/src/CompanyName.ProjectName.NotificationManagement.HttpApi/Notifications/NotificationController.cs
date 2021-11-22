using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    [Route("Notification")]
    public class NotificationController : AbpController, INotificationAppService
    {
        private readonly INotificationAppService _notificationAppService;

        public NotificationController(
            INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }


        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        [HttpPost("Text")]
        [SwaggerOperation(summary: "分页查询普通消息", Tags = new[] { "Notification" })]
        public Task<PagedResultDto<PagingNotificationListOutput>>
            GetPageTextNotificationByUserIdAsync(
                PagingNotificationListInput listInput)
        {
            return _notificationAppService.GetPageTextNotificationByUserIdAsync(listInput);
        }

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        [HttpPost("BroadCast")]
        [SwaggerOperation(summary: "分页查询广播消息", Tags = new[] { "Notification" })]
        public Task<PagedResultDto<PagingNotificationListOutput>>
            GetPageBroadCastNotificationByUserIdAsync(
                PagingNotificationListInput listInput)
        {
            return _notificationAppService.GetPageBroadCastNotificationByUserIdAsync(listInput);
        }

        [HttpPost("Read")]
        [SwaggerOperation(summary: "消息设置为已读", Tags = new[] { "Notification" })]
        public Task SetReadAsync(SetReadInput input)
        {
            return _notificationAppService.SetReadAsync(input);
        }

        [HttpPost("Create")]
        [SwaggerOperation(summary: "创建消息-测试使用", Tags = new[] { "Notification" })]
        public Task CreateAsync(CreateNotificationInput input)
        {
            return _notificationAppService.CreateAsync(input);
        }

        [HttpPost("SendMessage")]
        [SwaggerOperation(summary: "发送消息-测试使用", Tags = new[] { "Notification" })]
        public Task SendMessageAsync(string title, string content, MessageType messageType, List<string> users)
        {
            return _notificationAppService.SendMessageAsync(title, content, messageType, users);
        }
    }
}