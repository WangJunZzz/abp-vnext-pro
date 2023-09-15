FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV TZ=Asia/Shanghai
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/host/MyCompanyName.MyProjectName.HttpApi.Host"
RUN dotnet build "MyCompanyName.MyProjectName.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompanyName.MyProjectName.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .    
ENTRYPOINT ["dotnet", "MyCompanyName.MyProjectName.HttpApi.Host.dll"]

