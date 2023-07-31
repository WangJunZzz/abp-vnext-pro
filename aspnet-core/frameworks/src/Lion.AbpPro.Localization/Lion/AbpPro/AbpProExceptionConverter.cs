using Microsoft.Extensions.Options;

namespace Lion.AbpPro;

public class AbpProExceptionConverter : IAbpProExceptionConverter, ITransientDependency
{
    private readonly AbpExceptionLocalizationOptions _options;
    private readonly IStringLocalizerFactory _localizerFactory;

    public AbpProExceptionConverter(
        IOptions<AbpExceptionLocalizationOptions> options,
        IStringLocalizerFactory localizerFactory)
    {
        _options = options.Value;
        _localizerFactory = localizerFactory;
    }

    public virtual string TryToLocalizeExceptionMessage(Exception exception)
    {
        if (!(exception is IHasErrorCode exceptionWithErrorCode))
        {
            return exception.Message;
        }

        if (exceptionWithErrorCode.Code.IsNullOrWhiteSpace() ||
            !exceptionWithErrorCode.Code.Contains(":"))
        {
            return exception.Message;
        }

        var codeNamespace = exceptionWithErrorCode.Code.Split(':')[0];

        var localizationResourceType = _options.ErrorCodeNamespaceMappings.GetOrDefault(codeNamespace);
        if (localizationResourceType == null)
        {
            return exception.Message;
        }

        var stringLocalizer = _localizerFactory.Create(localizationResourceType);
        var localizedString = stringLocalizer[exceptionWithErrorCode.Code];
        if (localizedString.ResourceNotFound)
        {
            return exception.Message;
        }

        var localizedValue = localizedString.Value;

        if (exception.Data != null && exception.Data.Count > 0)
        {
            foreach (var key in exception.Data.Keys)
            {
                localizedValue = localizedValue.Replace("{" + key + "}", exception.Data[key]?.ToString());
            }
        }

        return localizedValue;
    }
}