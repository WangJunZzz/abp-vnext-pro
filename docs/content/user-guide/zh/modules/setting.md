# 设置管理

[官方Setting模块参考文档](https://docs.abp.io/zh-Hans/abp/latest/Settings)

配置系统是在启动时配置应用程序很好的方式. 除了配置之外, ABP提供了另外一种设置和获取应用程序设置的方式.
设置存储在动态数据源(通常是数据库)中的键值对. 设置系统预构建了用户,租户,全局和默认设置方法并且可以进行扩展.

## 定义设置
使用设置之前需要定义它. ABP是 模块化的, 不同的模块可以拥有不同的设置. 只需要实现SettingDefinitionProvider类既可. 示例如下:

!!! info "和官方Setting模块区别，值添加了2个属性，一个分组，一个组件类型"

```csharp
public class CustomSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
     context.Add(
        new SettingDefinition(
                AbpProSettings.Other.Github,
                "https://github.com/WangJunZzz/abp-vnext-pro",
                L("DisplayName:" + AbpProSettings.Other.Github),
                L("Description:" + AbpProSettings.Other.Github)
            )
            // 分组
            .WithProperty(AbpProSettings.Group.Default,AbpProSettings.Group.OtherManagement) 
            // 前端组件类型
            .WithProperty(AbpProSettings.ControlType.Default,AbpProSettings.ControlType.TypeText)); 
    }
}
```

- SettingDefinition 类具有以下属性:
    - Name: 应用程序中设置的唯一名称. 是具有约束的唯一属性, 在应用程序获取/设置此设置的值 (设置名称定义为常量而不是魔法字符串是个好主意).
    - DefaultValue: 设置的默认值.
    - DisplayName: 本地化的字符串,用于在UI上显示名称.
    - Description: 本地化的字符串,用于在UI上显示描述.

- 上面添加了2个属性，为了适配vue前端，一个设置Setting属于哪个分组，一个是根据Setting的类型指定对应的前端组件，比如字符串就是,Input组件。
    - 支持以下组件：Text，CheckBox，Number

## 读取设置值
ISettingProvider 用于获取指定设置的值或所有设置的值. 示例用法:
```csharp
public class MyService
{
    private readonly ISettingProvider _settingProvider;

    //Inject ISettingProvider in the constructor
    public MyService(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public async Task FooAsync()
    {
        //Get a value as string.
        string userName = await _settingProvider.GetOrNullAsync("Smtp.UserName");

        //Get a bool value and fallback to the default value (false) if not set.
        bool enableSsl = await _settingProvider.GetAsync<bool>("Smtp.EnableSsl");

        //Get a bool value and fallback to the provided default value (true) if not set.
        bool enableSsl = await _settingProvider.GetAsync<bool>(
            "Smtp.EnableSsl", defaultValue: true);

        //Get a bool value with the IsTrueAsync shortcut extension method
        bool enableSsl = await _settingProvider.IsTrueAsync("Smtp.EnableSsl");

        //Get an int value or the default value (0) if not set
        int port = (await _settingProvider.GetAsync<int>("Smtp.Port"));

        //Get an int value or null if not provided
        int? port = (await _settingProvider.GetOrNullAsync("Smtp.Port"))?.To<int>();
    }
}
```
> ISettingProvider 是非常常用的服务,一些基类中(如IApplicationService)已经将其属性注入. 这种情况下可以直接使用SettingProvider.

ISettingProvider 使用设置值提供程序来获取设置值. 如果值提供程序无法获取设置值,则会回退到下一个值提供程序.
DefaultValueSettingValueProvider: 从设置定义的默认值中获取值.
ConfigurationSettingValueProvider: 从IConfiguration服务中获取值.
GlobalSettingValueProvider: 获取设置的全局值.
TenantSettingValueProvider: 获取当前租户的设置值.
UserSettingValueProvider: 获取当前用户的设置值.

> 设置回退系统从底部 (用户) 到 顶部(默认) 方向起作用.



