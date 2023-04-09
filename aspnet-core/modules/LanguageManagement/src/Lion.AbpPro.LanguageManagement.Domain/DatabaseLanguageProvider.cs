using Lion.AbpPro.LanguageManagement.Languages;

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
            return new LanguageListCacheItem(_objectMapper.Map<List<Language>, List<LanguageInfo>>(languages));
        });
        return result.Languages;
    }
}