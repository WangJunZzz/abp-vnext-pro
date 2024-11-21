---
outline: deep
---
# 站内信模块
- 通过站内信模块，可以发布公告、通知、消息等。
- 通过站内信模块，用户可以发送站内信给其他用户。

## 安装
- 站内信模块需要安装以下 NuGet 包：
- Lion.Abp.NotificationManagement.Application
- Lion.Abp.NotificationManagement.Application.Contracts
- Lion.Abp.NotificationManagement.Domain
- Lion.Abp.NotificationManagement.Domain.Shared
- Lion.Abp.NotificationManagement.EntityFrameworkCore
- Lion.Abp.NotificationManagement.HttpApi
- Lion.Abp.NotificationManagement.HttpApi.Client
- 添加 DependsOn(typeof(NotificationManagementXxxModule)) 特性到对应模块。
- <div style="color:red">在EntityFrameworkCore层添加数据库配置在AbpProDbContext.cs的OnModelCreating()方法中添加builder.ConfigureNotificationManagement();</div>
- 执行ef 迁移
    - dotnet ef migrations add AddNotification
    - dotnet ef database update

## 功能
- 可以发送warning、information、error类型的站内信。
- 可以发送广播类型的站内信。
- 可以发送给指定人员。

### 后端发布通告
- 注入INotificationAppService既可以发送公告
```csharp
namespace Lion.AbpPro.NotificationManagement.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        /// <summary>
        /// 发送警告文本消息
        /// </summary>
        Task SendCommonWarningMessageAsync(SendCommonMessageInput input);

        /// <summary>
        /// 发送普通文本消息
        /// </summary>
        Task SendCommonInformationMessageAsync(SendCommonMessageInput input);

        /// <summary>
        /// 发送错误文本消息
        /// </summary>
        Task SendCommonErrorMessageAsync(SendCommonMessageInput input);

        /// <summary>
        /// 发送警告广播消息
        /// </summary>
        Task SendBroadCastWarningMessageAsync(SendBroadCastMessageInput input);

        /// <summary>
        /// 发送正常广播消息
        /// </summary>
        Task SendBroadCastInformationMessageAsync(SendBroadCastMessageInput input);

        /// <summary>
        /// 发送错误广播消息
        /// </summary>
        Task SendBroadCastErrorMessageAsync(SendBroadCastMessageInput input);
    }
}
```
- 调用结果展示
![](https://lion-abp-pro.oss-cn-shenzhen.aliyuncs.com/foods/e91cdf2c5ba24164b18e92cf876a2e00_gonggao.png)

### 前端发布通告
- 系统管理->通告管理->发布通告

## 配置
- 前端在env配置websocket地址
- VITE_WEBSOCKET_URL: 'http://xxx:44317/signalr/notification'

::: tip 注意
- 部署之后需要启用websocket功能，否则前端无法连上signalr
:::