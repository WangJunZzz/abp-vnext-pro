<p align="center">
  <a href="https://github.com/WangJunZzz/abp-vnext-pro">
      <img src="https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/abp/06.jpg">
  </a>
</p>

<h1 align="center">Abp Vnext Pro</h1>



## 🔗 链接

- [文档地址](http://doc.cncore.club/)
- [视频教程](https://www.bilibili.com/video/BV1pt4y1E7aZ)


## 📦 快速开始

- 安装Cli，[Git 仓库](https://github.com/WangJunZzz/Lion.AbpPro.Cli)
```bash
dotnet tool install Lion.AbpPro.Cli -g
```
### 三个项目模板
- 生成源码版本

```bash
lion.abp new abp-vnext-pro -c 公司名称 -p 项目名称 -o 输出路径(可选) -v 版本号(可选)
```

- nuget包形式的网关基础版本
   -  abp自带的所有模块，pro的通知模块，数据字典模块 以及ocelot网关。

```bash
lion.abp new abp-vnext-pro-basic -c 公司名称 -p 项目名称 -v 版本(可选) -o 项目输出路径(可选).
```

- nuget包形式的基础版本
   - abp自带的所有模块，pro的通知模块，数据字典模块 无ocelot网关

```bash
lion.abp new abp-vnext-pro-basic-no-ocelot -c 公司名称 -p 项目名称 -v 版本(可选) -o 项目输出路径(可选).
```



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


## 🤝 如何贡献

- 通过 [Issue](https://github.com/WangJunZzz/abp-vnext-pro/issues) 报告:bug:或进行咨询。
- QQ 群：686933575

## 赞助
如果你觉得这个项目对你有帮助，你可以帮作者买一杯咖啡表示支持!
![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/donate.png)
