namespace Lion.AbpPro.LanguageManagement.LanguageTexts.Caches;

public class LanguageTexCacheInvalidator : ILocalEventHandler<EntityChangedEventData<LanguageText>>, ITransientDependency
{
    private readonly IDistributedCache<LanguageTextCacheItem> _distributedCache;

    public LanguageTexCacheInvalidator(IDistributedCache<LanguageTextCacheItem> distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<LanguageText> eventData)
    {
        await _distributedCache.RemoveAsync(LanguageTextCacheItem.CalculateCacheKey(eventData.Entity.ResourceName, eventData.Entity.CultureName));
    }
}