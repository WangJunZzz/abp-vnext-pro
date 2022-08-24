<p align="center">
  <a href="https://github.com/WangJunZzz/abp-vnext-pro">
      <img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/06.jpg">
  </a>
</p>

<h1 align="center">Abp Vnext Pro</h1>

<div align="center">

Abp Vnext Vue 实现版本

<table>
    <tr>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/4.4/4.4login.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/01.png"/></td>
    </tr>
        <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/02.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/03.png"/></td>
    </tr>
        </tr>
        <tr>
         <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/04.png"/></td>
        <td><img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/05.png"/></td>
    </tr>
</table>

</div>

## 🔗 链接

- [文档地址](http://doc.cncore.club/)
- [演示地址 ](http://120.24.194.14:8012/)

## ✨ 系统功能

- [x] 用户管理
- [x] 角色管理
- [x] 审计日志
- [x] 后台任务
- [x] 集成事件
- [x] SinglaR 消息通知(站内信)
- [x] 多语言
- [x] 数据字典
- [x] 容器化部署
- [x] 单元测试
- [x] ES 日志
- [x] Setting 管理
- [x] 多租户
- [x] 文件管理

## 📦 安装

- 安装Cli，Git 仓库(https://github.com/WangJunZzz/Lion.AbpPro.Cli)
```bash
dotnet tool install Lion.AbpPro.Cli -g
```
### 三个项目模板
- 生成源码版本

```bash
lion.abp new abp-vnext-pro -c 公司名称 -p 项目名称 -o 输出路径(可选) -v 版本号(可选)
```

- nuget包形式的基础版本,包括abp自带的所有模块，已经pro的通知模块，数据字典模块 以及ocelot网关

```bash
lion.abp new abp-vnext-pro-basic -c 公司名称 -p 项目名称 -v 版本(默认LastRelease) -o 项目输出路径(可选).
```

- nuget包形式的基础版本,包括abp自带的所有模块，已经pro的通知模块，数据字典模块 无ocelot网关
```bash
lion.abp new abp-vnext-pro-basic-no-ocelot -c 公司名称 -p 项目名称 -v 版本(默认LastRelease) -o 项目输出路径(可选).
```

## 🗺 开发路线

查看[开发路线](https://github.com/WangJunZzz/abp-vnext-pro/projects)来了解我们的开发计划。

## 🤝 如何贡献

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
- QQ 群：686933575
