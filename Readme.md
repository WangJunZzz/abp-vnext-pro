<div align="center">

[Abp Vnext Pro](https://github.com/WangJunZzz/abp-vnext-pro) 的 Vue 实现版本  
开箱即用的中后台前端/设计解决方案

<table>
    <tr>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/user.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/role.png"/></td>
    </tr>
    <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/setting.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/dic.png"/></td>
    </tr>
</table>
</div>

### 知识点

- .Net Core5.0
- Abp Vnext 4.x ,
- Ant Design, Vue2.x
- Mysql,Redis,Hangfire,ES(日志可选),Nocas(可选,选择nocas分支),RabbitMq(未集成,计划中)
- 微服务架构设计, DDD 实践
- 容器化 CI CD
- Xunit 单元测试

### 系统功能

- 用户管理
- 角色管理
- 设置管理
- 字典管理
- 后台作业
- ES 日志
- 暂时不支持多租户管理(后续考虑)

### 对接思路

- 前端
  - 通过 token 调用 /api/abp/application-configuration 获取应用级别信息，包括权限，多语言，保存在 Store 中;
  - 多语言基于前端，后端 Api 的多语言基于 abp 自带的;
  - 菜单权限封装，在 route/config.js 下配置菜单,属性 meta.policy 不传或者等于\*代表不验证权限
  - 按钮权限，在 utils/permission.js 下，isGranted('策略名'),例如：v-if="isGranted('AbpIdentity.Roles.Create')"
- 后端
  - 项目不一定要基于 IdentityServer4,所以新增了一个登陆方法,生成 Token.
  - 集成 ES 日志
  - 集成 Redis
  - 集成 Hangfire
  - 集成 SettingUI

### 使用

#### clone

```bash
$ git clone https://github.com/WangJunZzz/abp-vnext-pro
```

#### 后端

- 修改 Mysql,Redis 连接字符串
- 迁移数据：执行 Zzz.DbMigrator

#### 前端

```bash
- yarn or npm i
- npm run dev
```

#### 该项目也是一个模板项目

- 本地安装

```bash
# 在cotnent目录下执行
 dotnet new -i .\content
```

- 新建项目

```bash
dotnet new Zzz --name 你的项目名称(不支持名词xxx.xxx,只支持一级)
```

#### 参与贡献

非常欢迎你的贡献，你可以通过以下方式和我们一起共建 :star2:：

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
