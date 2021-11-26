using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationManagementDomainException : UserFriendlyException
    {
        public NotificationManagementDomainException(string message, string code = null, string details = null,
            Exception innerException = null, LogLevel logLevel = LogLevel.Warning) : base(message, code, details,
            innerException, logLevel)
        {
        }

        public NotificationManagementDomainException(SerializationInfo serializationInfo, StreamingContext context) :
            base(serializationInfo, context)
        {
        }
    }
}