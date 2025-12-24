using Lion.AbpPro.LanguageManagement.Languages;
using Mapster;

namespace Lion.AbpPro.LanguageManagement;

[Dependency(ReplaceServices = true)]
public class DatabaseLanguageProvider : ILanguageProvider, ITransientDependency 
{
    private readonly ILanguageManager _languageManager;
    private readonly IDistributedCache<LanguageListCacheItem> _distributedCache;
    private readonly IObjectMapper _objectMapper;

    public DatabaseLanguageProvider(ILanguageManager languageManager, IDistributedCache<LanguageListCacheItem> distributedCache, IObjectMapper objectMapper)
    {
        _languageManager = languageManager;
        _distributedCache = distributedCache;
        _objectMapper = objectMapper;
    }

    public virtual async Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync()
    {
        var result = await _distributedCache.GetOrAddAsync(LanguageListCacheItem.CalculateCacheKey(), async () =>
        {
            var languages = await _languageManager.ListAsync(true);
            var languageInfos = new List<LanguageInfo>();
            foreach (var language in languages)
            {
                var temp = new LanguageInfo(language.CultureName, language.UiCultureName, language.DisplayName);
                languageInfos.Add(temp);
            }
            return new LanguageListCacheItem(languageInfos);
        });
        return result.Languages;
    }
}