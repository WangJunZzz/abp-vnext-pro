using System;

namespace CompanyName.ProjectName.NotificationManagement.Notifications.Dtos
{
    public class SetReadInput
    {
        public Guid Id { get; set; }
        
        public Guid ReceiveId { get; set; }
    }
}