using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Zzz.Cache
{
    public class CacheManger: ICacheManger
    {
        private readonly IDistributedCache _cache;

        public CacheManger(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetAsync(string key)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            return Encoding.UTF8.GetString(await _cache.GetAsync(key));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(await _cache.GetAsync(key)));
        }

        public async Task SetAsync(string key, string value, int expiratioTime = 7200)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            Check.NotNullOrWhiteSpace(value, nameof(value));
            var options = new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiratioTime) };
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(value),options);
        }

        public async Task SetAsync<T>(string key, T value, int expiratioTime = 7200)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            var options = new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiratioTime) };
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
   
        }

        public async Task SetOptionsAsync(string key, string value, DistributedCacheEntryOptions options = null)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            Check.NotNullOrWhiteSpace(value, nameof(value));
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        }


        public async Task SetOptionsAsync<T>(string key, T value, DistributedCacheEntryOptions options = null)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        }
    }
}
