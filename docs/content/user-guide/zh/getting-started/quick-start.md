# 快速开始

## 先决条件
- [dotnet core 6.0.202](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [nodejs 16+](https://nodejs.org/zh-cn/)
- [pnpm](https://www.pnpm.cn/installation)
- [mysql](https://dev.mysql.com/downloads/installer/)
- [redis](https://redis.io/docs/getting-started/installation/)
- [rabbitmq 可选](https://www.rabbitmq.com/download.html)

!!! info "Docker一键安装Mysql|Redis|Rabbitmq"

    快速搭建必要环境，下载 [docker-compose.yaml](docker-compose.yaml) , 执行: docker-compose up -d


## 创建新项目

### 直接Clone

``` bash
 git clone https://github.com/WangJunZzz/abp-vnext-pro.git
```

或者

### GUI创建项目
![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/gui.png)

### 后端
- 修改 HttpApi.Host-> appsettings.json 配置
    - Mysql 连接字符串
    - Redis 连接字符串
    - RabbitMq(如果不需要启用设置为 false)
    - Es 地址即可(如果没有 es 也可以运行,只是前端 es 日志页面无法使用而已，不影响后端项目启动)
- 修改 IdentityServer-> appsettings.json 数据库连接字符串
- 修改 DbMigrator-> appsettings.json 数据库连接字符串
- 右键单击.DbMigrator项目,设置为启动项目运行，按F5(或Ctrl + F5) 运行应用程序. 它将具有如下所示的输出:
![](../../../img/migrating.png)

!!! note 种子数据

    初始的种子数据在数据库中创建了 admin 用户(密码为1q2w3E*) 用于登录应用程序. 所以, 对于新数据库至少使用 .DbMigrator 一次.


!!! note Ocelot网关

    如果不需要使用Ocelot网关可以移除Lion.AbpPro.WebGateway项目，前端接口地址直接修改为Lion.AbpPro.HttpApi.Host的接口地址。

!!! note IdentityServer4

    如果不需要使用IdentityServer4可以移除Lion.AbpPro.IdentityServer项目，请参考如何如何移除IdentityServer4 

- 多项目启动(HttpApi.Host,IdentityServer,WebGateway)，就能看到后台服务登陆页面，如下：
![](../../../img/login.png)

  


## 前端
- [Vben Admin 文档](https://vvbin.cn/doc-next/)

### 安装npm包

```bash
pnmp install 
```

### 启动项目

```bash
npm run dev
```
