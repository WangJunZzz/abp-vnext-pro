<div align="center">

​																			[Abp Vnext Pro](https://github.com/WangJunZzz/abp-vnext-pro) 的 Vue3 实现版本  开箱即用的中后台前端/设计解决方案

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

基于ABP Vnext4.4.0的微服务架构，基于DDD思想开发，基于vue3.0,Typescript,Antd 的后台管理框架，适用于大型分布式业务系统和企业后台。
[演示地址http://120.24.194.14:8012/](http://120.24.194.14:8012/) 

- 用户名：admin 密码：1q2w3E*

- 警告：不要修改IdentitySever4 客户端管理的Vue3客户端设置，不然影响IdentityServer4登录

#### 系统功能

- [x] 用户管理
- [x] 角色管理
- [x] 审计日志
- [x] 后台任务(hangfire)
- [x] 集成事件(dotnetcore.cap)
- [x] IdentityServer4
  	- [x] 客户端管理
  	- [x] Api资源管理
  	- [x] ApiScope管理
  	- [x] Identity资源管理
- [x] SinglaR消息通知
- [x] 多语言
- [x] FreeSql
- [x] 数据字典(UI暂时没有)
- [x] 容器化部署
- [x] 单元测试
- [x] ES日志
- [ ] 多租户
- [ ] 组织机构



#### 前端特别说明

- abp 有提供默认api为什么要重写user，role，permission接口?
  - 因为前端的调用后台接口通过nswag生成了代理，api提供的接口地址导致生成代理冲突所以重写了
  - 在后端有接口变化请在前端执行npm run nswag 重新生成代理
  - 建议后端的方法设置为post，前端会生成typecript的接口
  - 后端使用swagger的时候tag请不要用中文   [SwaggerOperation(summary: "获取所有角色", Tags = new[] { "Role" })]
  - 前端代理生成在src/services下，如何使用请参考用户模块
  
- 前端
  - 多语言基于前端，后端 Api 的多语言基于 abp 自带的;
  - 配置菜单,属性 meta.policy 不传代表不验证权限
  - 按钮权限，v-auth 例如：v-auth=('AbpIdentity.Roles.Create')

### 使用

- 下载代码生成器，Git仓库(https://github.com/WangJunZzz/abp-vnext-pro-gui)

  ![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/gui.png)

- 启动
  - 前端yarn
  
  - 后端修改mysql和redis连接字符串
  
  - 执行迁移控制台程序
  
  - 启动HttpApi.Host和IdentityServer4
  
    

#### 参与贡献

非常欢迎你的贡献，你可以通过以下方式和我们一起共建 :star2:：

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
- QQ群：686933575
