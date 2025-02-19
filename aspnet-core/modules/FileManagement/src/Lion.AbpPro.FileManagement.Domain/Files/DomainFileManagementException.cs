namespace Lion.AbpPro.FileManagement.Files
{
    public class DomainFileManagementException : BusinessException
    {
        public DomainFileManagementException(string code = null, string message = null, string details = null, Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(code, message, details,
            innerException,
            logLevel
        )
        {
        }
    }
}