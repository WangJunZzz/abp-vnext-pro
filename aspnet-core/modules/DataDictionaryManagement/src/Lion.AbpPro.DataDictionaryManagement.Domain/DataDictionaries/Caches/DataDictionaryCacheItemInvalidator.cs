using System.Threading.Tasks;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dto;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Caches
{
    public class DataDictionaryCacheItemInvalidator :
        ILocalEventHandler<EntityChangedEventData<DataDictionary>>,
        ITransientDependency
    {
        private readonly IDistributedCache<DataDictionaryDto> _cache;

        public DataDictionaryCacheItemInvalidator(IDistributedCache<DataDictionaryDto> cache)
        {
            _cache = cache;
        }

        public async Task HandleEventAsync(EntityChangedEventData<DataDictionary> eventData)
        {
            await _cache.RemoveAsync(
                DataDictionaryDto.CalculateCacheKey(eventData.Entity.Id, eventData.Entity.Code));
            await _cache.RemoveAsync(
                DataDictionaryDto.CalculateCacheKey(eventData.Entity.Id, null));
            await _cache.RemoveAsync(
                DataDictionaryDto.CalculateCacheKey(null, eventData.Entity.Code));
        }
    }
}