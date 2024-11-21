---
outline: deep
---

# 分布式锁
- ABP vNext Pro 已集成 Redis 做为分布式锁。
- [ABP vNext分布式锁官方文档](https://abp.io/docs/latest/framework/infrastructure/distributed-locking)

## 集成
- 引用Volo.Abp.DistributedLocking包,并且添加模块依赖.
- 引用DistributedLock.Redis包
```csharp
namespace AbpDemo
{
    [DependsOn(typeof(AbpDistributedLockingModule))]
    public class MyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
        
            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connection = ConnectionMultiplexer
                    .Connect(configuration["Redis:Configuration"]);
                return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            });
        }
    }
}
```

## sample
```csharp
namespace AbpDemo
{
    public class MyService : ITransientDependency
    {
        private readonly IAbpDistributedLock _distributedLock;
		public MyService(IAbpDistributedLock distributedLock)
        {
            _distributedLock = distributedLock;
        }
        
        public async Task MyMethodAsync()
        {
            await using (var handle = await _distributedLock.TryAcquireAsync("MyLockName"))
            {
                if (handle != null)
                {
                    // 获取到锁执行业务逻辑
                }
                else
                {
                    // 未获取到锁
                    // 处理异常
                }
            }   
        }
    }
}
```

::: tip 注意
TryAcquireAsync参数说明:
- name (string， required）：锁的唯一名称。不同的命名锁用于访问不同的资源。
- timeout (TimeSpan）：等待获取锁的超时值。默认值为TimeSpan.Zero ，这意味着如果锁已由另一个应用程序拥有，则它不会等待。
- cancellationToken：稍后可以触发以取消操作的取消令牌。
:::