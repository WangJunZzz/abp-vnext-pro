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

非常欢迎你的加入！提一个 Issue 或者提交一个 Pull Request。

**Pull Request:**

1. Fork 代码!
2. 创建自己的分支: `git checkout -b feat/xxxx`
3. 提交你的修改: `git commit -am 'feat(function): add xxxxx'`
4. 推送您的分支: `git push origin feat/xxxx`
5. 提交`pull request`

## Git 贡献提交规范

- 参考 [vue](./apps/vue/.github/COMMIT_CONVENTION.md) 规范 ([Angular](https://github.com/conventional-changelog/conventional-changelog/tree/master/packages/conventional-changelog-angular))

  - `feat` 增加新功能
  - `fix` 修复问题/BUG
  - `style` 代码风格相关无影响运行结果的
  - `perf` 优化/性能提升
  - `refactor` 重构
  - `revert` 撤销修改
  - `test` 测试相关
  - `docs` 文档/注释
  - `chore` 依赖更新/脚手架配置修改等
  - `workflow` 工作流改进
  - `ci` 持续集成
  - `types` 类型定义文件更改
  - `wip` 开发中

## ✒️交流
- QQ 群：686933575

## 💖赞助
- 如果你觉得这个项目对你有帮助，你可以帮作者买一杯咖啡表示支持!
![](https://blog-resouce.oss-cn-shenzhen.aliyuncs.com/images/donate.png)
- 欢迎Star
