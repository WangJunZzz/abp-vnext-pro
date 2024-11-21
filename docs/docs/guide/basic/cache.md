---
outline: deep
---

# 缓存
- ABP 框架扩展了 [ASP.NET Core 的分布式缓存系统](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed).
- ABP vNext Pro 已集成 Redis 做为缓存。
- [ABP vNext缓存官方文档](https://abp.io/docs/latest/framework/fundamentals/caching)

## 配置

```json [appsetting.json]
"Redis":
  {
     "Configuration": "localhost,password=1q2w3E*,defaultdatabase=1"
  }
```

### AbpDistributedCacheOptions

示例：为应用程序设置缓存键前缀

```cs [AbpProHttpApiHostModule.cs]
Configure<AbpDistributedCacheOptions>(options =>
{
    options.KeyPrefix = "MyApp1";
});
```

### 可用选项

- `HideErrors` (`bool`, 默认: `true`): 启用/禁用隐藏从缓存服务器写入/读取值时的错误.
- `KeyPrefix` (`string`, 默认: `null`): 如果你的缓存服务器由多个应用程序共同使用, 则可以为应用程序的缓存键设置一个前缀. 在这种情况下, 不同的应用程序不能覆盖彼此的缓存内容.
- `GlobalCacheEntryOptions` (`DistributedCacheEntryOptions`): 用于设置保存缓内容却没有指定选项时, 默认的分布式缓存选项 (例如 `AbsoluteExpiration` 和 `SlidingExpiration`). `SlidingExpiration`的默认值设置为 20 分钟.

## 使用方式

示例: 在缓存中存储图书名称和价格

```csharp
namespace MyProject
{
    public class BookCacheItem
    {
        public string Name { get; set; }

        public float Price { get; set; }
    }
}
```

你可以注入 `IDistributedCache<BookCacheItem>` 服务用于 get/set `BookCacheItem` 对象.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace MyProject
{
    public class BookService : ITransientDependency
    {
        private readonly IDistributedCache<BookCacheItem> _cache;

        public BookService(IDistributedCache<BookCacheItem> cache)
        {
            _cache = cache;
        }

        public async Task<BookCacheItem> GetAsync(Guid bookId)
        {
            return await _cache.GetOrAddAsync(
                bookId.ToString(), //缓存键
                async () => await GetBookFromDatabaseAsync(bookId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                }
            );
        }

        private Task<BookCacheItem> GetBookFromDatabaseAsync(Guid bookId)
        {
            //TODO: 从数据库获取数据
        }
    }
}
```

- 示例服务代码中的 `GetOrAddAsync()` 方法从缓存中获取图书项. `GetOrAddAsync`是 ABP 框架在 ASP.NET Core 分布式缓存方法中添增的附加方法.
- 如果没有在缓存中找到图书,它会调用工厂方法 (本示例中是 `GetBookFromDatabaseAsync`)从原始数据源中获取图书项.
- `GetOrAddAsync` 有一个可选参数 `DistributedCacheEntryOptions` , 可用于设置缓存的生命周期.

## 批量操作

ABP 的分布式缓存接口定义了以下批量操作方法,当你需要在一个方法中调用多次缓存操作时,这些方法可以提高性能

- `SetManyAsync` 和 `SetMany` 方法可以用来向缓存中设置多个值.
- `GetManyAsync` 和 `GetMany` 方法可以用来从缓存中获取多个值.
- `GetOrAddManyAsync` 和 `GetOrAddMany` 方法可以用来从缓存中获取并添加缺少的值.
- `RefreshManyAsync` 和 `RefreshMany` 方法可以来用重置多个值的滚动过期时间.
- `RemoveManyAsync` 和 `RemoveMany` 方法可以用来从缓存中删除多个值.

> 这些不是标准的 ASP.NET Core 缓存方法, 所以某些提供程序可能不支持. [ABP Redis 集成包]实现了它们. 如果提供程序不支持,会回退到 `SetAsync` 和 `GetAsync` ... 方法(循环调用).
