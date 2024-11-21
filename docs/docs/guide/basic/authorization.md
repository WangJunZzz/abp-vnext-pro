---
outline: deep
---

# 权限
## 后端
### 定义权限
- 在Application.Contracts层的Permissions文件夹下定义权限
- 创建一个继承自 `BookStorePermissionDefinitionProvider` 的类,如下所示:
```csharp
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
::: danger 注意
ABP 会自动发现这个类,不需要进行配置!
:::

### 设置接口权限
示例:
```csharp
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
- `[Authorize("BookStore_Author_Create")]` 定义了一个策略 (参阅 [基于策略的授权](https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies)),它用于检查当前用户的权限."BookStore_Author_Create"



### 多租户

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
    multiTenancySide: MultiTenancySides.Tenant 
);
```

## 前端权限
::: tip 注意
- 前端登录成功之后调用/api/abp/application-configuration接口获取当前用户的权限信息，并根据权限信息控制页面的显示和按钮的显示。
- auth.grantedPolicies下有当前用户的所有权限code。
:::

### 菜单权限

::: code-group
```ts [Vben2.8]
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

```bash [Vben5]
import type { RouteRecordRaw } from 'vue-router';

import { BasicLayout } from '#/layouts';

const routes: RouteRecordRaw[] = [
  {
    component: BasicLayout,
    meta: {
      icon: 'lucide:layout-dashboard',
      order: -1,
      title: '系统管理',
      authority: ['AbpIdentity'],
    },
    name: 'system',
    path: '/system',
    children: [
      {
        name: 'abpUser',
        path: 'user',
        component: () => import('#/views/system/abpUser/index.vue'),
        meta: {
          // affixTab: true,
          icon: 'ph:user',
          title: '用户管理',
          authority: ['AbpIdentity.Users'],
        },
      },
      {
        name: 'abpRole',
        path: 'role',
        component: () => import('#/views/system/abpRole/index.vue'),
        meta: {
          icon: 'oui:app-users-roles',
          title: '角色管理',
          authority: ['AbpIdentity.Roles'],
        },
      }
    ],
  },
];

export default routes;

```

:::



### 按钮权限

::: code-group
```ts [Vben2.8]
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

```bash [Vben5]

<template>
  <Page auto-content-height title="用户管理">
    <Grid>
      <template #toolbar-actions>
        <Space>
          <Button
            type="primary"
            v-access:code="'AbpIdentity.Users.Create'"
            @click="openAddModal"
          >
            新增
          </Button>
          <Button
            type="primary"
            v-access:code="'AbpIdentity.Users.Export'"
            @click="exportData"
          >
            导出
          </Button>
        </Space>
      </template>

      <template #isActive="{ row }">
        <component
          :is="
            h(
              Tag,
              { color: row.isActive ? 'green' : 'red' },
              row.isActive ? '启用' : '禁用',
            )
          "
        />
      </template>
      <template #action="{ row }">
        <Space>
          <Button
            size="small"
            type="primary"
            v-access:code="'AbpIdentity.Users.Update'"
            @click="onEdit(row)"
          >
            编辑
          </Button>
          <Dropdown>
            <Button size="small"> 更多操作 </Button>
            <template #overlay>
              <Menu>
                <MenuItem @click="onLock(row)">
                  <Button
                    size="small"
                    type="link"
                    v-access:code="'AbpIdentity.Users.Enable'"
                  >
                    {{ row.isActive ? '禁用' : '启用' }}
                  </Button>
                </MenuItem>
                <MenuItem @click="onDel(row)">
                  <Button
                    danger
                    size="small"
                    type="link"
                    v-access:code="'AbpIdentity.Users.Delete'"
                  >
                    删除
                  </Button>
                </MenuItem>
              </Menu>
            </template>
          </Dropdown>
        </Space>
      </template>
    </Grid>
  </Page>
</template>


```

:::
