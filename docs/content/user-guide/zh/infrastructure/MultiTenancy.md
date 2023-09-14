# 多租户

## 定义实体

你可以在你的实体中实现 IMultiTenant 接口来实现多租户,例如:

```C#

namespace MyCompany.MyProject
{
    public class Product : AggregateRoot, IMultiTenant
    {
        public Guid? TenantId { get; set; } //IMultiTenant 定义了 TenantId 属性

        public string Name { get; set; }

        public float Price { get; set; }
    }
}
```

实现 IMultiTenant 接口,需要在实体中定义一个 TenantId 的属性

## 获取当前租户

你的代码中可能需要获取当前租户(先不管它具体是怎么取得的).对于这种情况你可以注入并使用 ICurrentTenant 接口.例如:

```C#

namespace MyCompany.MyProject
{
    public class MyService : ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;

        public MyService(ICurrentTenant currentTenant)
        {
            _currentTenant = currentTenant;
        }

        public void DoIt()
        {
            var tenantId = _currentTenant.Id;
            //在你的代码中使用tenantId
        }
    }
}
```

## 改变当前租户

```csharp

namespace MultiTenancyDemo.Products
{
    public class ProductManager : DomainService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductManager(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<long> GetProductCountAsync(Guid? tenantId)
        {
            using (CurrentTenant.Change(tenantId))
            {
                return await _productRepository.GetCountAsync();
            }
        }
    }
}
```

## 确定当前租户

多租户的应用程序运行的时候首先要做的就是确定当前租户.
Volo.Abp.MultiTenancy 只提供了用于确定当前租户的抽象(称为租户解析器),但是并没有现成的实现.
Volo.Abp.AspNetCore.MultiTenancy 已经实现了从当前 Web 请求(从子域名,请求头,cookie,路由...等)中确定当前租户.本文后面会介绍 Volo.Abp.AspNetCore.MultiTenancy.

## 自定义租户解析器

你可以像下面这样,在你模块的 ConfigureServices 方法中将自定义解析器并添加到 AbpTenantResolveOptions 中:

```C#

namespace MyCompany.MyProject
{
    [DependsOn(typeof(AbpMultiTenancyModule))]
    public class MyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Add(new MyCustomTenantResolveContributor());
            });

            //...
        }
    }
}
```

`MyCustomTenantResolveContributor`必须像下面这样实现 ITenantResolveContributor 接口:

```C#
namespace MyCompany.MyProject
{
    public class MyCustomTenantResolveContributor : ITenantResolveContributor
    {
        public override Task ResolveAsync(ITenantResolveContext context)
        {
            context.TenantIdOrName = ... //从其他地方获取租户id或租户名字...
        }
    }
}
```

如果能确定租户 id 或租户名字可以在租户解析器中设置 TenantIdOrName.如果不能确定,那就空着让下一个解析器来确定它.

## 租户信息

ITenantStore 跟 TenantConfiguration 类一起工作,并且包含了几个租户属性:

- Id:租户的唯一 Id.
- Name: 租户的唯一名称.
- ConnectionStrings:如果这个租户有专门的数据库来存储数据.它可以提供数据库的字符串(它可以具有默认的连接字符串和每个模块的连接字符串).

## 多租户中间件

Volo.Abp.AspNetCore.MultiTenancy 包含了多租户中间件...

```C#
app.UseMultiTenancy();
```

## 从 Web 请求中确定当前租户

Volo.Abp.AspNetCore.MultiTenancy 添加了下面这些租户解析器,从当前 Web 请求(按优先级排序)中确定当前租户.

- CurrentUserTenantResolveContributor: 如果当前用户已登录,从当前用户的声明中获取租户 Id. 出于安全考虑,应该始终将其做为第一个 Contributor.
- QueryStringTenantResolveContributor: 尝试从 query string 参数中获取当前租户,默认参数名为"\_\_tenant".
- RouteTenantResolveContributor:尝试从当前路由中获取(URL 路径),默认是变量名是"\_\_tenant".所以,如果你的路由中定义了这个变量,就可以从路由中确定当前租户.
- HeaderTenantResolveContributor: 尝试从 HTTP header 中获取当前租户,默认的 header 名称是"\_\_tenant".
- CookieTenantResolveContributor: 尝试从当前 cookie 中获取当前租户.默认的 Cookie 名称是"\_\_tenant".

> 如果你使用 nginx 作为反向代理服务器,请注意如果`TenantKey`包含下划线或其他特殊字符可能存在问题, 请参考:
> http://nginx.org/en/docs/http/ngx_http_core_module.html#ignore_invalid_headers > http://nginx.org/en/docs/http/ngx_http_core_module.html#underscores_in_headers

可以使用 AbpAspNetCoreMultiTenancyOptions 修改默认的参数名"\_\_tenant".例如:

```C#
services.Configure<AbpAspNetCoreMultiTenancyOptions>(options =>
{
    options.TenantKey = "MyTenantKey";
});
```
