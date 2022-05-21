using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryDomainException : BusinessException
    {
        public DataDictionaryDomainException(string code = null, string message = null, string details = null, Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(code, message, details,
            innerException,
            logLevel
        )
        {
        }

        public DataDictionaryDomainException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
    }
}