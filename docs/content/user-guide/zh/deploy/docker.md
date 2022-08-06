# Docker部署
## Lion.AbpPro.HttpApi.Host 

- 在Lion.AbpPro.HttpApi.Host目录下执行

```bash
dotnet public -o publish
```

- 生成 Docker 镜像

```bash
docker build -t Lion.AbpPro.HttpApi.Host .
```

- 运行容器

```bash
docker run -itd --name Lion.AbpPro.HttpApi.Host -p 8011:80 Lion.AbpPro.HttpApi.Host
```

- Dockerfile 文件

```yml
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# 创建目录
RUN mkdir /app

COPY publish /app

RUN echo "deb http://mirrors.aliyun.com/debian/ bullseye main non-free contrib" > /etc/apt/sources.list && \
    echo "deb-src http://mirrors.aliyun.com/debian/ bullseye main non-free contrib" >> /etc/apt/sources.list && \
    echo "deb http://mirrors.aliyun.com/debian-security/ bullseye-security main" >> /etc/apt/sources.list && \
    echo "deb-src http://mirrors.aliyun.com/debian-security/ bullseye-security main" >> /etc/apt/sources.list && \
    echo "deb http://mirrors.aliyun.com/debian/ bullseye-updates main non-free contrib" >> /etc/apt/sources.list && \
    echo "deb-src http://mirrors.aliyun.com/debian/ bullseye-updates main non-free contrib" >> /etc/apt/sources.list && \
    echo "deb http://mirrors.aliyun.com/debian/ bullseye-backports main non-free contrib" >> /etc/apt/sources.list && \
    echo "deb-src http://mirrors.aliyun.com/debian/ bullseye-backports main non-free contrib" >> /etc/apt/sources.list && \
    apt-get update && \
    apt-get install libgdiplus libc6-dev -y && \
    ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll && \
    ln -s /usr/lib/x86_64-linux-gnu/libdl.so /usr/lib/libdl.dll && apt-get clean
    
# 设置工作目录
WORKDIR /app

# 暴露80端口
EXPOSE 80
# 设置时区 .net6 才有这个问题
ENV TZ=Asia/Shanghai

# 设置环境变量
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Lion.AbpPro.HttpApi.Host.dll"]

```

## Lion.AbpPro.IdentityServer 

- 在Lion.AbpPro.IdentityServer目录下执行

```bash
dotnet public -o publish
```

- 生成 Docker 镜像

```bash
docker build -t Lion.AbpPro.IdentityServer .
```

- 运行容器

```bash
docker run -itd --name Lion.AbpPro.IdentityServer -p 8013:80 Lion.AbpPro.IdentityServer
```

- Dockerfile 文件

```yml
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# 创建目录
RUN mkdir /app

COPY publish /app

# 设置工作目录
WORKDIR /app

# 暴露80端口
EXPOSE 80

# 设置时区 .net6 才有这个问题
ENV TZ=Asia/Shanghai

# 设置环境变量
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Lion.AbpPro.IdentityServer.dll"]

```


## Lion.AbpPro.WebGateway

- 在Lion.AbpPro.WebGateway目录下执行

```bash
dotnet public -o publish
```

- 生成 Docker 镜像

```bash
docker build -t Lion.AbpPro.WebGateway .
```

- 运行容器

```bash
docker run -itd --name Lion.AbpPro.WebGateway -p 8013:80 Lion.AbpPro.WebGateway
```

- Dockerfile 文件

```yml
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# 创建目录
RUN mkdir /app

COPY publish /app

# 设置工作目录
WORKDIR /app

# 暴露80端口
EXPOSE 80

# 设置时区 .net6 才有这个问题
ENV TZ=Asia/Shanghai

# 设置环境变量
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Lion.AbpPro.WebGateway.dll"]
```


## Vue3
- 修改env.production 接口地址为以上你发布的地址
- 打包项目

```bash
npm run build
```

- 生产Docker镜像

```bash
docker build -t Lion.AbpPro.Vue3 .
```

- 运行容器

```bash
docker run -itd --name Lion.AbpPro.Vue3 -p 8012:80 Lion.AbpPro.Vue3
```

- Dockerfile 文件

```yml
FROM nginx:1.17.3-alpine as base
EXPOSE 80
COPY /_nginx/nginx.conf /etc/nginx/nginx.conf
COPY /_nginx/env.js /etc/nginx/env.js
COPY /_nginx/default.conf /etc/nginx/conf.d/default.conf
COPY /dist/ /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]
```