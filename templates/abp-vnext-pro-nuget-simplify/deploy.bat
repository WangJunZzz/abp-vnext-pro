@echo off
@echo build vben28
cd vben28&&pnpm install&&pnpm run build
cd  %~dp0
cd ./aspnet-core/src/MyCompanyName.MyProjectName.DbMigrator && dotnet publish -o publish
cd  %~dp0
cd ./aspnet-core/host/MyCompanyName.MyProjectName.HttpApi.Host && dotnet publish -o publish
cd  %~dp0



