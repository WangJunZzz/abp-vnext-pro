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


