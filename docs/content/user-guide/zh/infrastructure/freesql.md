### CQRS
CQRS:命令查询职责隔离,命令是指 插入、修改、删除，就是更改数据的动作.通过 Freesql 解决单一数据模型带来的查询尴尬场面。
当前架构下，Freesql 和 ef 不在一个事务，最好实现就是用来做查询，比如分页查询。
![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/cqrs.png)

## 配置

```csharp
public class AbpProFreeSqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var connectionString = configuration.GetConnectionString("Default");
        var freeSql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.MySql, connectionString)
            .Build();

        context.Services.AddSingleton<IFreeSql>(freeSql);
    }
}
```

## 使用

- 在 Domain 层添加接口

```csharp
public interface IUserFreeSqlBasicRepository
{
    Task<List<UserOutput>> GetListAsync();
}
```

- 在 Freesql 层添加实现

```csharp
public class UserFreeSqlBasicRepository : FreeSqlBasicRepository, IUserFreeSqlBasicRepository
{
    public async Task<List<UserOutput>> GetListAsync()
    {
        var sql = "select id from AbpUsers";
        var result = await FreeSql.Select<UserOutput>()
        .WithSql(sql)
        .ToListAsync();
        return result;
    }
}
```
