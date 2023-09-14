# 授权

授权用于在应用程序中判断是否允许用户执行某些特定的操作.

ABP 扩展了[ASP.NET Core 授权](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/introduction), 将权限添加为自动[策略](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies)并且使授权系统在 [应用服务](Application-Services.md) 同样可用.

## Authorize Attribute

ASP.NET Core 定义了 [Authorize](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/simple)特性用于在控制器,控制器方法以及页面上授权. 现在 ABP 将它带到了[应用服务](Application-Services.md).

示例:

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;

namespace Acme.BookStore
{
    [Authorize]
    public class AuthorAppService : ApplicationService, IAuthorAppService
    {
        public Task<List<AuthorDto>> GetListAsync()
        {
            ...
        }

        [AllowAnonymous]
        public Task<AuthorDto> GetAsync(Guid id)
        {
            ...
        }

        [Authorize("BookStore_Author_Create")]
        public Task CreateAsync(CreateAuthorDto input)
        {
            ...
        }
    }
}

```

- `Authorize`用户必须登陆到应用程序才可以访问 `AuthorAppService` 中的方法. 所以`GetListAsync` 方法仅可用于通过身份验证的用户.
- `AllowAnonymous` 禁用身份验证. 所以 `GetAsync` 方法任何人都可以访问,包括未授权的用户.
- `[Authorize("BookStore_Author_Create")]` 定义了一个策略 (参阅 [基于策略的授权](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies)),它用于检查当前用户的权限."BookStore_Author_Create" 是一个策略名称. 如果你想要使用策略的授权方式,需要在 ASP.NET Core 授权系统中预先定义它.

## 定义权限

创建一个继承自 `PermissionDefinitionProvider` 的类,如下所示:

```csharp
using Volo.Abp.Authorization.Permissions;

namespace Acme.BookStore.Permissions
{
    public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup("BookStore");

            myGroup.AddPermission("BookStore_Author_Create");
        }
    }
}
```

> ABP 会自动发现这个类,不需要进行配置!

在 `Define` 方法中添加权限组或者获取已存在的权限组,并向权限组中添加权限.
在定义权限后就可以在 ASP.NET Core 权限系统中当做策略名称使用. 在角色的权限管理模态框中同样可以看到:
![](../../../img/auth.png){: .zoom}

## 多租户

在定义新权限时可以设置多租户选项. 有下面三个值:

- Host: 权限仅适用于宿主.
- Tenant: 权限仅适用于租户.
- Both (默认): 权限适用与宿主和租户.

> 如果你的应用程序不是多租户的,可以忽略这个选项.

`AddPermission` 方法的第三个参数用于设置多租户选项:

```csharp
myGroup.AddPermission(
    "BookStore_Author_Create",
    LocalizableString.Create<BookStoreResource>("Permission:BookStore_Author_Create"),
    multiTenancySide: MultiTenancySides.Tenant //set multi-tenancy side!
);
```

## 前端权限

### 菜单权限

```ts
import type { AppRouteModule } from "/@/router/types";
import { LAYOUT } from "/@/router/constant";
import { t } from "/@/hooks/web/useI18n";
const tenant: AppRouteModule = {
  path: "/tenant",
  name: "Tenant",
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: "ant-design:contacts-outlined",
    title: t("routes.tenant.tenantManagement"),
  },
  children: [
    {
      path: "Tenant",
      name: "Tenant",
      component: () => import("/@/views/tenants/Tenant.vue"),
      meta: {
        title: t("routes.tenant.tenantList"),
        icon: "ant-design:switcher-filled",
        policy: "AbpTenantManagement.Tenants", //菜单权限
      },
    },
  ],
};

export default tenant;
```

### 按钮权限

```ts
<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'ant-design:edit-outlined',
              auth: 'AbpIdentity.Users.Update', // 按钮权限
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              auth: 'AbpIdentity.Users.Delete', // 按钮权限
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
            {
              auth: 'System.Users.Enable', // 按钮权限
              label: !record.isActive
                ? t('common.enabled')
                : t('common.disEnabled'),
              onClick: handleLock.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <CreateAbpUser
      @register="registerCreateAbpUserModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <EditAbpUser
      @register="registerEditAbpUserModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>
```
