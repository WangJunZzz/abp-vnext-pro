---
outline: deep
---

# 本地事件
- 本地事件总线允许服务发布和订阅in-process events.这意味着，如果两个服务 （发布者和订阅者） 在同一进程中运行，则它是合适的。
- [ABP vNext本地事件官方文档](https://abp.io/docs/latest/framework/infrastructure/event-bus/local)

## 发布事件
- 有两种发布本地事件的方法
### 使用 ILocalEventBus 发布事件
```csharp
namespace AbpDemo
{
    public class MyService : ITransientDependency
    {
        private readonly ILocalEventBus _localEventBus;

        public MyService(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }
        
        public virtual async Task ChangeStockCountAsync(Guid productId, int newCount)
        {
            await _localEventBus.PublishAsync(
                new StockCountChangedEvent
                {
                    ProductId = productId,
                    NewCount = newCount
                }
            );
        }
    }
}
```

```csharp
namespace AbpDemo
{
    public class StockCountChangedEvent
    {
        public Guid ProductId { get; set; }
        
        public int NewCount { get; set; }
    }
}
```
### 在实体/聚合根类中发布事件
- AggregateRoot类定义添加新的本地事件，该事件在将聚合根对象保存（创建、更新或删除）到数据库中时发布
```csharp
namespace AbpDemo
{
    public class Product : AggregateRoot<Guid>
    {
        public string Name { get; set; }
        
        public int StockCount { get; private set; }

        private Product() { }

        public Product(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public void ChangeStockCount(int newCount)
        {
            StockCount = newCount;
            
            AddLocalEvent(
                new StockCountChangedEvent
                {
                    ProductId = Id,
                    NewCount = newCount
                }
            );
        }
    }
}
```

## 预定义事件
- abp官方为我们提供了一些预定义的事件,如：EntityCreatedEventData、EntityDeletedEventData、EntityUpdatedEventData,EntityChangedEventData等。

::: tip 事件说明
    - EntityCreatedEventData<T>在成功创建实体后立即发布。
    - EntityUpdatedEventData<T>在实体成功更新后立即发布。
    - EntityDeletedEventData<T>在成功删除实体后立即发布。
    - EntityChangedEventData<T>在成功创建、更新或删除实体后立即发布。
:::

### 订阅预定义事件
```csharp
public class ChangedAbpUserLocalEventHandler: ILocalEventHandler<EntityChangedEventData<AbpUser>>, ITransientDependency
{
    private readonly IDistributedEventBus _distributedEventBus;

    public ChangedAbpUserLocalEventHandler(IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
    }

    public async Task HandleEventAsync(EntityChangedEventData<AbpUser> eventData)
    {
       // todo: handle event
    }
}
```
### 它是如何实现的？
- 当您将更改保存到数据库时，将发布预构建事件;
- 对于 EF Core,它们发布在DbContext.SaveChanges上
- 对于 MongoDB,当您调用存储库的,或方法时,会发布它们(因为MongoDB没有更改跟踪系统).所以在InsertAsync,UpdateAsync,DeleteAsync.
