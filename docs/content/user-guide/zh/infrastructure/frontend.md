# 前端
[Vben Admin 文档](https://vvbin.cn/doc-next/)


## 代码生成
!!! info "前端接口，参数，自动生成，全部采用 Post 方式"
- 所有访问后端接口代码自动生成 [NSwag](https://github.com/RicoSuter/NSwag)



###  配置代理的地址

- nswag->nswag.json

```json
  "documentGenerator": {
    "fromDocument": {
      // 代理地址，只有生成的时候用，不区分环境
      "url": "http://localhost:44315/swagger/v1/swagger.json", 
    }
  }
```

- 如果接口参数或者返回值有改变，需要重新生成代理，执行:

```bash
npm run nswag
```

### 后端Api格式

```csharp
// 一定要打Tags，因为前端会根据这个生成代理类
// 建议参数都封装为一个Input
[SwaggerOperation(summary: "登录", Tags = new[] {"Account"})]
public Task<LoginOutput> LoginAsync(LoginInput input)
{
    return _loginAppService.LoginAsync(input);
}
```

## 前端多环境
  - .env.development 和.env.production
  - VITE_API_URL:后端接口地址
  - VITE_AUTH_URL:IdentityServer接口地址

## 权限配置

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

```vue
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
              label: !record.isActive ? t('common.enabled') : t('common.disEnabled'),
              onClick: handleLock.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <CreateAbpUser @register="registerCreateAbpUserModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
    <EditAbpUser @register="registerEditAbpUserModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
  </div>
</template>
```
