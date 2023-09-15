# Docker 部署

## 后端

- 在 aspnetcore 目录下执行
- 修改 appsetting.Production.json 配置
  - 数据库连接
  - Redis 连接
  - Rabbitmq 连接(可选)

### Dockerfile

```yaml
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV TZ=Asia/Shanghai
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/services/host/Lion.AbpPro.HttpApi.Host"
RUN dotnet build "Lion.AbpPro.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lion.AbpPro.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lion.AbpPro.HttpApi.Host.dll"]
```

### 构建镜像

```bash
docker build -t Lion.AbpPro.HttpApi.Host .
```

### 启动容器

```bash
docker run -itd --name Lion.AbpPro.HttpApi.Host -p 8011:80 Lion.AbpPro.HttpApi.Host
```

## 前端

- 修改 env.production 接口地址为以上你发布的地址
- 打包项目

### Dockerfile

```yml
FROM node:16-alpine as build-stage
WORKDIR /app
COPY . ./
ENV NODE_OPTIONS=--max-old-space-size=16384
RUN npm install pnpm -g
RUN pnpm i
RUN pnpm build


FROM nginx:1.17.3-alpine as production-stage
COPY --from=build-stage app/_nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-stage app/_nginx/env.js /etc/nginx/env.js
COPY --from=build-stage app/_nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage app/dist/ /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
```

### 构建镜像

```bash
docker build -t Lion.AbpPro.Vue3 .
```

### 启动容器

```bash
docker run -itd --name Lion.AbpPro.Vue3 -p 8012:80 Lion.AbpPro.Vue3
```

!!! WARNING "检查跨域设置,请查看跨域文档"
