# 介绍

Lion.AbpPro 是Abp Vnext 的Vue3版本实现，同时也是免费开源。它有助于提高开发效率，属于开箱即用的后台管理系统，也能适用微服务。

## 后端项目结构
```bash
├── Directory.Build.props nuget 版本控制
├── frameworks # 公共模块
│       ├── CAP # dotnetcore.cap
│       └── Extensions # 自定义扩展
├── gateways # 网关
│       └── Lion.AbpPro.WebGateway # 基于ocelot网关
├── modules # 模块
│       ├── DataDictionaryManagement # 数据字典
│       ├── FileManagement # 基于阿里云的文件服务
│       └── NotificationManagement # 通知服务
├── services # 公共静态资源目录
│       ├── host # 启动模块
│           ├── CompanyName.ProjectName.HttpApi.Host # admin ui host
│           └── CompanyName.ProjectName.IdentityServer # IdentityServer host
│       ├── src  # 源码
│           └── CompanyName.ProjectName.DbMigrator # 迁移控制台程序
│       └── test # 单元测试
├── shared # 公共Host
│       ├── Lion.AbpPro.Shared.Hosting.Gateways # 网关host模块
│       └── Lion.AbpPro.Shared.Hosting.Microservices # 服务host模块
```

## 前端项目结构
```bash
├── _nginx # docker 打包
├── build # 打包脚本相关
│   ├── config # 配置文件
│   ├── generate # 生成器
│   ├── script # 脚本
│   └── vite # vite配置
├── mock # mock文件夹
├── public # 公共静态资源目录
├── src # 主目录
│   ├── api # 接口文件
│   ├── assets # 资源文件
│   │   ├── icons # icon sprite 图标文件夹
│   │   ├── images # 项目存放图片的文件夹
│   │   └── svg # 项目存放svg图片的文件夹
│   ├── components # 公共组件
│   ├── design # 样式文件
│   ├── directives # 指令
│   ├── enums # 枚举/常量
│   ├── hooks # hook
│   │   ├── component # 组件相关hook
│   │   ├── core # 基础hook
│   │   ├── event # 事件相关hook
│   │   ├── setting # 配置相关hook
│   │   └── web # web相关hook
│   ├── layouts # 布局文件
│   │   ├── default # 默认布局
│   │   ├── iframe # iframe布局
│   │   └── page # 页面布局
│   ├── locales # 多语言
│   ├── logics # 逻辑
│   ├── main.ts # 主入口
│   ├── router # 路由配置
│   ├── services # Nswag生成的代理
│   │   ├── ServiceProxies.ts # Nswag生成的代理
│   │   ├── ServiceProxyBase.ts # Nswag生成的代理拦截器
│   ├── settings # 项目配置
│   │   ├── componentSetting.ts # 组件配置
│   │   ├── designSetting.ts # 样式配置
│   │   ├── encryptionSetting.ts # 加密配置
│   │   ├── localeSetting.ts # 多语言配置
│   │   ├── projectSetting.ts # 项目配置
│   │   └── siteSetting.ts # 站点配置
│   ├── store # 数据仓库
│   ├── utils # 工具类
│   └── views # 页面
├── test # 测试
│   └── server # 测试用到的服务
│       ├── api # 测试服务器
│       ├── upload # 测试上传服务器
│       └── websocket # 测试ws服务器
├── types # 类型文件
├── vite.config.ts # vite配置文件
└── windi.config.ts # windcss配置文件
```
## 相关视频

待完善

## 相关文章

待完善
