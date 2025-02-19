namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos
{
    public class DeleteMessageInput
    {
        public Guid Id { get; set; }


        /// <summary>
        /// 接受者Id
        /// </summary>
        public Guid? ReceiverUserId { get; set; }
    }
}