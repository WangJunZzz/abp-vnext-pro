---
outline: deep
---

# 种子数据

- [ABP vNext种子数据官方文档](https://abp.io/docs/latest/framework/infrastructure/data-seeding)

## 介绍

- 使用数据库的某些应用程序(或模块),可能需要有一些初始数据才能够正常启动和运行. 例如管理员用户和角色必须在一开始就可用. 否则你就无法登录到应用程序创建新用户和角色.
- 数据种子也可用于测试的目的,你的自动测试可以假定数据库中有一些可用的初始数据.

## IDataSeedContributor

将数据种子化到数据库需要实现`IDataSeedContributor`接口.

示例: 如果没有图书,则向数据库播种一个初始图书

```csharp
namespace Acme.BookStore
{
    public class BookStoreDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public BookStoreDataSeedContributor(
            IRepository<Book, Guid> bookRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _bookRepository = bookRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _bookRepository.GetCountAsync() > 0)
                {
                    return;
                }

                var book = new Book(
                    id: _guidGenerator.Create(),
                    name: "The Hitchhiker's Guide to the Galaxy",
                    type: BookType.ScienceFiction,
                    publishDate: new DateTime(1979, 10, 12),
                    price: 42
                );

                await _bookRepository.InsertAsync(book);
            }
        }
    }
}
```

- `IDataSeedContributor` 定义了 `SeedAsync` 方法用于执行 数据种子逻辑.
- 通常检查数据库是否已经存在种子数据.
- 你可以注入服务,检查数据播种所需的任何逻辑.

> 数据种子贡献者由 ABP 框架自动发现,并作为数据播种过程的一部分执行.

## 模块化

一个应用程序可以具有多个种子数据贡献者(`IDataSeedContributor`)类. 任何可重用模块也可以实现此接口播种其自己的初始数据.
