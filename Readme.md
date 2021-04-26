<div align="center">

​																			[Abp Vnext Pro](https://github.com/WangJunZzz/abp-vnext-pro) 的 Vue3 实现版本  开箱即用的中后台前端/设计解决方案

<table>
    <tr>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/login.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/user.png"/></td>
    </tr>
    <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/role.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/settings.png"/></td>
    </tr>
</table>

</div>

### 说明

- main分支为主要开发分支，后续都基于此分支维护，该分支前端基于vue3.0,Typescript,如果要使用Vue请切换到Vue2分支。

- .Net Core5.0
- Abp Vnext 4.2 ,
- Ant Design, Vben Admin [前端文档](https://vvbin.cn/doc-next/)
- Mysql,Redis,Hangfire,ES(日志可选)
- 微服务架构设计, DDD 实践
- 容器化 CI CD
- Xunit 单元测试

### 对接思路

- 前端
  - 通过 token 调用 /api/abp/application-configuration 获取应用级别信息，包括权限，多语言，保存在 Store 中;
  - 多语言基于前端，后端 Api 的多语言基于 abp 自带的;
  - 配置菜单,属性 meta.policy 不传代表不验证权限
  - 按钮权限，v-auth 例如：v-auth=('AbpIdentity.Roles.Create')
- 后端
  - 项目不一定要基于 IdentityServer4,所以新增了一个登陆方法,生成 Token.
  - IdentityServer4的已经独立出来,也会开源。

### 使用

- 下载代码生成器，Git仓库(https://github.com/WangJunZzz/abp-vnext-pro-gui)

  ![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/gui.png)

- 下载模板之后再当前项目src\AbpVnextPro.GUI\bin\Debug\net5.0-windows\decompression可以看到生成的源码
- 启动
  - 前端yarn
  
  - 后端修改mysql和redis连接字符串
  
  - 执行tools下的迁移控制台程序
  
  - 启动host下httpapi.host即可
  
  - host下的public可以忽略，这个用来做暴露第三方接口的，通过id4授权。
  
    

#### 参与贡献

非常欢迎你的贡献，你可以通过以下方式和我们一起共建 :star2:：

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
