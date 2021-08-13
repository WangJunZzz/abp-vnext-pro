using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Exceptions
{
    public class DataDictionaryDomainException : UserFriendlyException
    {
        public DataDictionaryDomainException(string message, string code = null, string details = null,
            Exception innerException = null, LogLevel logLevel = LogLevel.Warning) : base(message, code, details,
            innerException, logLevel)
        {
        }

        public DataDictionaryDomainException(SerializationInfo serializationInfo, StreamingContext context) : base(
            serializationInfo, context)
        {
        }
    }
}