namespace Lion.AbpPro.LanguageManagement;

public class LocalizationHelper : LanguageManagementDomainService, ILocalizationHelper
{
    private readonly AbpLocalizationOptions _abpLocalizationOptions;
    private readonly IExternalLocalizationStore _externalLocalizationStore;
    private readonly IStringLocalizerFactory _stringLocalizerFactory;

    public LocalizationHelper(
        IOptions<AbpLocalizationOptions> abpLocalizationOptions,
        IExternalLocalizationStore externalLocalizationStore,
        IStringLocalizerFactory stringLocalizerFactory)
    {
        _abpLocalizationOptions = abpLocalizationOptions.Value;
        _externalLocalizationStore = externalLocalizationStore;
        _stringLocalizerFactory = stringLocalizerFactory;
    }

    /// <summary>
    /// 资源是否有效
    /// </summary>
    public virtual async Task<bool> IsValidResourceName(string resourceName)
    {
        var resource = _abpLocalizationOptions
            .Resources
            .Values
            .Union(
                new[] { await _externalLocalizationStore.GetResourceOrNullAsync(resourceName) }
            )
            .FirstOrDefault(e => e.ResourceName == resourceName);
        if (resource == null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 获取所有资源名
    /// </summary>
    public virtual async Task<List<string>> GetAllResourceName()
    {
        var resources = _abpLocalizationOptions
            .Resources
            .Values
            .Select(x => x.ResourceName)
            .Union(
                await _externalLocalizationStore.GetResourceNamesAsync()
            );
        return await Task.FromResult(resources.Distinct().ToList());
    }

    public virtual IStringLocalizer GetLocalizer(LocalizationResource resource)
    {
        return _stringLocalizerFactory.Create(resource.ResourceType);
    }

    public virtual IStringLocalizer GetLocalizer(string recourseName)
    {
        return this.GetLocalizer(GetLocalizationResource(recourseName));
    }

    public virtual LocalizationResource GetLocalizationResource(string resourceName)
    {
        var localizationResource = _abpLocalizationOptions.Resources.Values.FirstOrDefault(r => r.ResourceName == resourceName);
        if (localizationResource == null)
        {
            throw new LanguageManagementDomainException(LanguageManagementErrorCodes.ResourceNotFound);
        }

        return localizationResource as LocalizationResource;
    }
}