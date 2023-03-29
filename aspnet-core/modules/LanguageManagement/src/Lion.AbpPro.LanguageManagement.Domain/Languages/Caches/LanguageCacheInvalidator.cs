namespace Lion.AbpPro.LanguageManagement.Languages.Caches;

/// <summary>
/// 当语言实体有改变的时候，清除缓存
/// </summary>
public class LanguageCacheInvalidator : ILocalEventHandler<EntityChangedEventData<Language>>, ITransientDependency
{
    private readonly IDistributedCache<LanguageListCacheItem> _distributedCache;

    public LanguageCacheInvalidator(IDistributedCache<LanguageListCacheItem> distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<Language> eventData)
    {
        await _distributedCache.RemoveAsync(LanguageListCacheItem.CalculateCacheKey());
    }
}