namespace Lion.AbpPro.ElasticSearch.Exceptions;

public class AbpProElasticSearchEntityNotFoundException : BusinessException
{
    public AbpProElasticSearchEntityNotFoundException(
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

    public AbpProElasticSearchEntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }
}