FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443
ENV TZ=Asia/Shanghai
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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

