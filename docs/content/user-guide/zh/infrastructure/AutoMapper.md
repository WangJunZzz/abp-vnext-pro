# 对象到对象映射

将对象映射到另一个对象是常用并且繁琐重复的工作,大部分情况下两个类都具有相同或相似的属性. ABP 提供了对象到对象映射的抽象并集成了[AutoMapper](http://automapper.org/)做为对象映射器.

## AutoMapper 集成

[AutoMapper](http://automapper.org/) 是最流行的对象到对象映射库之一. [Volo.Abp.AutoMapper](https://www.nuget.org/packages/Volo.Abp.AutoMapper)程序包使用 AutoMapper 实现了 `IObjectMapper`.

### 定义映射

AutoMapper 提供了多种定义类之间映射的方法. 有关详细信息请参阅[AutoMapper 的文档](https://docs.automapper.org).

其中定义一种映射的方法是创建一个[Profile](https://docs.automapper.org/en/stable/Configuration.html#profile-instances) 类. 例如:

```csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<User, UserDto>();
    }
}
```

然后使用`AbpAutoMapperOptions`注册配置文件:

```csharp
[DependsOn(typeof(AbpAutoMapperModule))]
public class MyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            //Add all mappings defined in the assembly of the MyModule class
            options.AddMaps<MyModule>();
        });
    }
}
```

`AddMaps` 注册给定类的程序集中所有的配置类,通常使用模块类. 它还会注册 [attribute 映射](https://docs.automapper.org/en/stable/Attribute-mapping.html).

### 配置验证

`AddMaps` 使用可选的 `bool` 参数控制[模块](Module-Development-Basics.md)的[配置验证](https://docs.automapper.org/en/stable/Configuration-validation.html):

```csharp
options.AddMaps<MyModule>(validate: true);
```

如果此选项默认是 `false` , 但最佳实践建议启用.
可以使用 `AddProfile` 而不是 `AddMaps` 来控制每个配置文件类的配置验证:

```csharp
options.AddProfile<MyProfile>(validate: true);
```

> 如果你有多个配置文件,并且只需要为其中几个启用验证,那么首先使用`AddMaps`而不进行验证,然后为你想要验证的每个配置文件使用`AddProfile`.

### 映射对象扩展

[对象扩展系统](Object-Extensions.md) 允许为已存在的类定义额外属性. ABP 框架提供了一个映射定义扩展可以正确的映射两个对象的额外属性.

```csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<User, UserDto>()
            .MapExtraProperties();
    }
}
```

如果两个类都是可扩展对象(实现了 `IHasExtraProperties` 接口),建议使用 `MapExtraProperties` 方法. 更多信息请参阅[对象扩展文档](Object-Extensions.md).

### 其他有用的扩展方法

有一些扩展方法可以简化映射代码.

#### 忽视审计属性

当你将一个对象映射到另一个对象时,通常会忽略审核属性.

假设你需要将 `ProductDto` ([DTO](Data-Transfer-Objects.md))映射到 Product[实体](Entities.md),该实体是从 `AuditedEntity` 类继承的(该类提供了 `CreationTime`, `CreatorId`, `IHasModificationTime` 等属性).

从 DTO 映射时你可能想忽略这些基本属性,可以使用 `IgnoreAuditedObjectPropertie()` 方法忽略所有审计属性(而不是手动逐个忽略它们):

```csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<ProductDto, Product>()
            .IgnoreAuditedObjectProperties();
    }
}
```

还有更多扩展方法, 如 `IgnoreFullAuditedObjectProperties()` 和 `IgnoreCreationAuditedObjectProperties()`,你可以根据实体类型使用.

> 请参阅[实体文档](Entities.md)中的"_基类和接口的审计属性_"部分了解有关审计属性的更多信息。

#### 忽视其他属性

在 AutoMapper 中,通常可以编写这样的映射代码来忽略属性:

```csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<SimpleClass1, SimpleClass2>()
            .ForMember(x => x.CreationTime, map => map.Ignore());
    }
}
```

我们发现它的长度是不必要的并且创建了 `Ignore()` 扩展方法:

```csharp
public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<SimpleClass1, SimpleClass2>()
            .Ignore(x => x.CreationTime);
    }
}
```

## 使用

```csharp
// 注入IObjectMapper
public virtual async Task<LanguageDto> GetAsync(string cultureName)
{
    var entity = await _languageRepository.FindAsync(cultureName);
    return ObjectMapper.Map<Language, LanguageDto>(entity);
}
```
