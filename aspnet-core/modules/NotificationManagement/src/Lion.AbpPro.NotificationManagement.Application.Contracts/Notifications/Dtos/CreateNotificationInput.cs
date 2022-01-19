using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lion.AbpPro.NotificationManagement.Notifications.Enums;

namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class CreateNotificationInput : IValidatableObject
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 接收人
        /// 如果消息类型是广播消息，接收人字段为空
        /// </summary>
        public List<Guid> ReceiveIds { get; set; }

        public CreateNotificationInput()
        {
            ReceiveIds = new List<Guid>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MessageType == MessageType.BroadCast && ReceiveIds.Count > 0)
            {
                yield return new ValidationResult("当消息类型为广播消息是，接收人列表只能为空", new[] {"ReceiveIds"});
            }
        }
    }
}