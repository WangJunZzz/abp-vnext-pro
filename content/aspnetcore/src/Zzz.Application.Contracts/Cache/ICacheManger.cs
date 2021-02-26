using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zzz.Cache
{
    public interface ICacheManger : ITransientDependency
    {


        Task<string> GetAsync(string key);


        Task<T> GetAsync<T>(string key);


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiratioTime">秒</param>
        /// <returns></returns>
        Task SetAsync(string key, string value, int expiratioTime = 7200);


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiratioTime">秒</param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, int expiratioTime = 7200);



        Task SetOptionsAsync(string key, string value, DistributedCacheEntryOptions options = null);


        Task SetOptionsAsync<T>(string key, T value, DistributedCacheEntryOptions options = null);

    }
}
