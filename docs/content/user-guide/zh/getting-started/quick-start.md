# 快速开始

## 先决条件

- [dotnet core 7.0.401](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [nodejs 16+](https://nodejs.org/zh-cn/)
- [pnpm](https://www.pnpm.cn/installation)
- [mysql](https://dev.mysql.com/downloads/installer/)
- [redis](https://redis.io/docs/getting-started/installation/)
- [rabbitmq 可选](https://www.rabbitmq.com/download.html)

## 安装 CLI 工具

```bash
dotnet tool install Lion.AbpPro.Cli -g
```

## 创建项目

- 项目选择

```bash
lion.abp new -t pro -c 公司名称 -p 项目名称  -o 输出路径(可选)
lion.abp new -t pro.all -c 公司名称 -p 项目名称  -o 输出路径(可选)
lion.abp new -t pro.simplify -c 公司名称 -p 项目名称  -o 输出路径(可选)
lion.abp new -t pro.module -c 公司名称 -p 项目名称  -m 模块名称  -o 输出路径(可选)
```

- 创建示例项目

```bash
# 创建一个公司名称为Acme项目名为BookStore的单体项目
lion.abp new -t pro.simplify -c Acme -p BookStore
```

### 后端修改配置

- 修改 HttpApi.Host-> appsettings.json 配置
  - Mysql 连接字符串
  - Redis 连接字符串
- 修改 DbMigrator-> appsettings.json 数据库连接字符串
- 右键单击.DbMigrator 项目,设置为启动项目运行，按 F5(或 Ctrl + F5) 运行应用程序. 它将具有如下所示的输出:
  ![](../../../img/migrating.png){: .zoom}

!!! note 种子数据

    初始的种子数据在数据库中创建了 admin 用户(密码为1q2w3E*) 用于登录应用程序. 所以, 对于新数据库至少使用 .DbMigrator 一次.

- 启动 HttpApi.Host，就能看到后台服务登陆页面，如下：
  ![](../../../img/login.png){: .zoom}

### 前端

- [Vben Admin 文档](https://vvbin.cn/doc-next/)

#### 安装依赖

```bash
pnpm install
```

#### 启动项目

```bash
pnpm run dev
```
