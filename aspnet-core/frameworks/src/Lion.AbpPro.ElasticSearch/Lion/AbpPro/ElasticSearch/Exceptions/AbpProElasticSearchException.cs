namespace Lion.AbpPro.ElasticSearch.Exceptions;

public class AbpProElasticSearchException : BusinessException
{
    public AbpProElasticSearchException(
        string code = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Error)
        : base(
            code,
            message,
            details,
            innerException,
            logLevel
        )
    {
    }

    public AbpProElasticSearchException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }
}