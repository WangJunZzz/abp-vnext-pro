---
outline: deep
---

# 登录
::: tip 注意
- ABP vNext Pro 没有集成 IdentityServer4 或者 OpenIddict,而是直接使用默认的 Asp Net Core Identity。
- 减少系统复杂度
- 后续会对接第三方登录，如：微信、QQ、微博等只要实现了 OAuth2 协议即可
:::


## 接口
- 地址：/api/app/account/login
- 方法：POST
- 请求参数：
```json
{
    "name":"admin",
    "password":"1q2w3E*"
}
```
- 返回结果：
```json
{
    "id": "3a15e986-c584-7fb6-d482-383d634204c5",
    "name": "admin",
    "userName": "admin",
    "token": "token",
    "roles": [
        "admin"
    ]
}
```
## 配置

```json [appsetting.json]
 "Jwt": {
    "Audience": "Lion.AbpPro",
    "SecurityKey": "dzehzRz9a8asdfasfdadfasdfasdfafsdadfasbasdf=",
    "Issuer": "Lion.AbpPro",
    "ExpirationTime": 30
  }
```
- Audience:接收对象
- Issuer:签发主体
- SecurityKey:密钥
- ExpirationTime:过期时间(单位小时)

## 多租户登录
- 登录之前通过租户名查找租户Id
- 把租户Id加入到header中
- 地址：Tenants/find
- 方法：POST
- 请求参数：
```json
{
    "name":"test" // 租户名
}
```
- 返回结果：
```json
{
    "success": true,
    "tenantId": "3a1640cf-fa8d-45d6-cfc5-7c0d28a7f2ef",
    "name": "test",
    "normalizedName": "TEST",
    "isActive": true
}
```
- 把租户Id加入到header中
```ts
request.headers['__tenant'] = userStore.tenant?.tenantId
```
::: tip 注意
- Abp会自动解析出租户，后续所有的接口请求，请求头都带上租户Id，Abp会根据租户Id，自动过滤数据。
- 如果使用独立数据库，会自动切换到对应的数据库。
:::


## 代码实现
```cs [AccountAppService.cs]
public virtual async Task<LoginOutput> LoginAsync(LoginInput input)
{
    var result = await _signInManager.PasswordSignInAsync(input.Name, input.Password, false, true);
    if (result.IsNotAllowed)
    {
        throw new BusinessException(BasicManagementErrorCodes.UserLockedOut);
    }
    if (!result.Succeeded)
    {
        throw new BusinessException(BasicManagementErrorCodes.UserOrPasswordMismatch);
    }
    var user = await _userManager.FindByNameAsync(input.Name);
    return await BuildResult(user);
}
```


