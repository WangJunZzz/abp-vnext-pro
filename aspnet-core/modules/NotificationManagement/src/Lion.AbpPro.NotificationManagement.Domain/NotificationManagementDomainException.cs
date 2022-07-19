namespace Lion.AbpPro.NotificationManagement
{
    public class NotificationManagementDomainException : BusinessException
    {
        public NotificationManagementDomainException(string code = null, string message = null, string details = null, Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(code, message, details,
            innerException,
            logLevel
        )
        {
        }

        public NotificationManagementDomainException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
    }
}