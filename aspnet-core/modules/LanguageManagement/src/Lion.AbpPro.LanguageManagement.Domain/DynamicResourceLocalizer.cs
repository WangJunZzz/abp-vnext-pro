using Lion.AbpPro.LanguageManagement.LanguageTexts;

namespace Lion.AbpPro.LanguageManagement;

/// <summary>
/// 动态资源
/// </summary>
public class DynamicResourceLocalizer : IDynamicResourceLocalizer, ISingletonDependency
{
    private readonly IDistributedCache<LanguageTextCacheItem> _distributedCache;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DynamicResourceLocalizer(IDistributedCache<LanguageTextCacheItem> distributedCache, IServiceScopeFactory serviceScopeFactory)
    {
        _distributedCache = distributedCache;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public LocalizedString GetOrNull(LocalizationResourceBase resource, string cultureName, string name)
    {
        var languageText = GetCacheLanguageText(resource, cultureName).GetAwaiter().GetResult();
        var value = languageText.Dictionary.GetOrDefault(name);
        if (value == null) return null;
        return new LocalizedString(name, value);
    }

    public void Fill(LocalizationResourceBase resource, string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        var languageText = GetCacheLanguageText(resource, cultureName).GetAwaiter().GetResult();
        foreach (var keyValuePair in languageText.Dictionary)
        {
            dictionary[keyValuePair.Key] = new LocalizedString(keyValuePair.Key, keyValuePair.Value);
        }
    }

    protected virtual async Task<LanguageTextCacheItem> GetCacheLanguageText(LocalizationResourceBase resource, string cultureName)
    {
        var result = await _distributedCache.GetOrAddAsync(LanguageTextCacheItem.CalculateCacheKey(resource.ResourceName, cultureName),
            async () => await CreateCacheLanguageText(resource, cultureName));
        return result;
    }

    protected virtual async Task<LanguageTextCacheItem> CreateCacheLanguageText(LocalizationResourceBase resource, string cultureName)
    {
        var result = new LanguageTextCacheItem();
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var languageTexts = await scope.ServiceProvider
                .GetRequiredService<ILanguageTextManager>()
                .FindAsync(cultureName, resource.ResourceName);
            foreach (var languageText in languageTexts)
            {
                result.Dictionary[languageText.Name] = languageText.Value;
            }
        }
        return result;
    }
}