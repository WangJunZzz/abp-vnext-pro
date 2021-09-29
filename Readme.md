<div align="center">

​ [Abp Vnext Pro](https://github.com/WangJunZzz/abp-vnext-pro) 的 Vue3 实现版本 开箱即用的中后台前端/设计解决方案

<table>
    <tr>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4login.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4roole.png"/></td>
    </tr>
    <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4hangfire.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4cap.png"/></td>
    </tr>
        <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4client.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4identity.png"/></td>
    </tr>
</table>

</div>

#### 项目简介

基于 ABP Vnext4.4.0 的微服务架构，基于 DDD 思想开发，基于 vue3.0,Typescript,Antd 的后台管理框架，适用于大型分布式业务系统和企业后台。

- [文档地址](http://cncore.club/)
- [演示地址 http://120.24.194.14:8012/](http://120.24.194.14:8012/)

- 用户名：admin 密码：1q2w3E\*

- 警告：不要修改 IdentitySever4 客户端管理的 Vue3 客户端设置，不然影响 IdentityServer4 登录

#### 系统功能

- [x] 用户管理
- [x] 角色管理
- [x] 审计日志
- [x] 后台任务(hangfire)
- [x] 集成事件(dotnetcore.cap)
- [x] IdentityServer4 - [x] 客户端管理 - [x] Api 资源管理 - [x] ApiScope 管理 - [x] Identity 资源管理
- [x] SinglaR 消息通知
- [x] 多语言
- [x] FreeSql
- [x] 数据字典(UI 暂时没有)
- [x] 容器化部署
- [x] 单元测试
- [x] ES 日志
- [x] Setting 管理
- [ ] 多租户
- [ ] 组织机构

#### 前端特别说明

- abp 有提供默认 api 为什么要重写 user，role，permission 接口?
  - 因为前端的调用后台接口通过 nswag 生成了代理，api 提供的接口地址导致生成代理冲突所以重写了
  - 在后端有接口变化请在前端执行 npm run nswag 重新生成代理
  - 建议后端的方法设置为 post，前端会生成 typecript 的接口
  - 后端使用 swagger 的时候 tag 请不要用中文 [SwaggerOperation(summary: "获取所有角色", Tags = new[] { "Role" })]
  - 前端代理生成在 src/services 下，如何使用请参考用户模块
- 前端
  - 多语言基于前端，后端 Api 的多语言基于 abp 自带的;
  - 配置菜单,属性 meta.policy 不传代表不验证权限
  - 按钮权限，v-auth 例如：v-auth=('AbpIdentity.Roles.Create')

### 使用

- 下载代码生成器，Git 仓库(https://github.com/WangJunZzz/abp-vnext-pro-gui)

  ![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/gui.png)

- 启动

  - 前端 yarn

  - 后端修改 mysql 和 redis 连接字符串

  - 执行迁移控制台程序

  - 启动 HttpApi.Host 和 IdentityServer4

#### 参与贡献

非常欢迎你的贡献，你可以通过以下方式和我们一起共建 :star2:：

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
- QQ 群：686933575
