FROM mcr.microsoft.com/dotnet/aspnet:5.0

# 创建目录
RUN mkdir /apps

COPY . /apps

# 设置工作目录
WORKDIR /apps

# 暴露80端口
EXPOSE 80

# 设置环境变量
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "Zzz.HttpApi.Host.dll"]