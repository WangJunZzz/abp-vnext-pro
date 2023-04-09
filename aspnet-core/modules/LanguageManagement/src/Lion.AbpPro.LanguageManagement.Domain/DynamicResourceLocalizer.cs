using Lion.AbpPro.LanguageManagement.LanguageTexts;

namespace Lion.AbpPro.LanguageManagement;

/// <summary>
/// 动态资源
/// </summary>
public class DynamicResourceLocalizer : IDynamicResourceLocalizer, ISingletonDependency
{
    private readonly ILanguageTextManager _languageTextManager;
    private readonly IDistributedCache<LanguageTextCacheItem> _distributedCache;

    public DynamicResourceLocalizer(ILanguageTextManager languageTextManager, IDistributedCache<LanguageTextCacheItem> distributedCache)
    {
        _languageTextManager = languageTextManager;
        _distributedCache = distributedCache;
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
        var languageTexts = await _languageTextManager.FindAsync(cultureName, resource.ResourceName);
        var result = new LanguageTextCacheItem();
        foreach (var languageText in languageTexts)
        {
            result.Dictionary[languageText.Name] = languageText.Value;
        }

        return result;
    }
}