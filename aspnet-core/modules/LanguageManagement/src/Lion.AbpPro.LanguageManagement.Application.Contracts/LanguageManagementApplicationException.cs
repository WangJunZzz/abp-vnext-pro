using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Lion.AbpPro.LanguageManagement;

public class LanguageManagementApplicationException : BusinessException
{
    public LanguageManagementApplicationException(
        string code = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
    }
}