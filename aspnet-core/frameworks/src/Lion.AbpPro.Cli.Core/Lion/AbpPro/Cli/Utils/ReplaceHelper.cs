namespace Lion.AbpPro.Cli.Utils;

public static class ReplaceHelper
{
    public static void ReplaceTemplates(string sourcePath, string oldCompanyName, string oldProjectName, string companyName, string projectName, string replaceSuffix)
    {
        try
        {
            RenameTemplate(sourcePath, oldCompanyName, oldProjectName, companyName, projectName, replaceSuffix);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"生成模板失败{ex.Message}");
        }
    }

    private static void ReplaceTemplates(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName,
        string replaceSuffix)
    {
        try
        {
            RenameTemplate(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"生成模板失败{ex.Message}");
        }
    }

    private static void RenameTemplate(string sourcePath, string oldCompanyName, string oldProjectName, string companyName, string projectName, string replaceSuffix)
    {
        RenameAllDirectories(sourcePath, oldCompanyName, oldProjectName, companyName, projectName);
     
        RenameAllFileNameAndContent(sourcePath, oldCompanyName, oldProjectName, companyName, projectName, replaceSuffix);
    }

    private static void RenameTemplate(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName,
        string replaceSuffix)
    {
        RenameAllDirectories(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName);
        RenameAllFileNameAndContent(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix);
    }

    private static void RenameAllDirectories(string sourcePath, string oldCompanyName, string oldProjectName, string companyName, string projectName)
    {
        var directories = Directory.GetDirectories(sourcePath);
        foreach (var subDirectory in directories)
        {
            RenameAllDirectories(subDirectory, oldCompanyName, oldProjectName, companyName, projectName);

            var directoryInfo = new DirectoryInfo(subDirectory);
            if (directoryInfo.Name.Contains(oldCompanyName) ||
                directoryInfo.Name.Contains(oldProjectName))
            {
                var oldDirectoryName = directoryInfo.Name;
                var newDirectoryName = oldDirectoryName.CustomReplace(oldCompanyName, oldProjectName, companyName, projectName);

                var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName, newDirectoryName);

                if (directoryInfo.FullName != newDirectoryPath)
                {
                    directoryInfo.MoveTo(newDirectoryPath);
                }
            }
        }
    }

    private static void RenameAllDirectories(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName)
    {
        var directories = Directory.GetDirectories(sourcePath);
        foreach (var subDirectory in directories)
        {
            RenameAllDirectories(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName);

            var directoryInfo = new DirectoryInfo(subDirectory);
            if (directoryInfo.Name.Contains(oldCompanyName) ||
                directoryInfo.Name.Contains(oldProjectName) ||
                directoryInfo.Name.Contains(oldModuleName))
            {
                var oldDirectoryName = directoryInfo.Name;
                var newDirectoryName = oldDirectoryName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName);

                var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName, newDirectoryName);

                if (directoryInfo.FullName != newDirectoryPath)
                {
                    directoryInfo.MoveTo(newDirectoryPath);
                }
            }
        }
    }

    private static void RenameAllFileNameAndContent(string sourcePath, string oldCompanyName, string oldProjectName, string companyName, string projectName, string replaceSuffix)
    {
        var list = new DirectoryInfo(sourcePath)
            .GetFiles()
            .Where(f => replaceSuffix.Contains(f.Extension))
            .ToList();

        var encoding = new UTF8Encoding(false);
        foreach (var fileInfo in list)
        {
            // 改文件内容
            var oldContents = File.ReadAllText(fileInfo.FullName, encoding);
            var newContents = oldContents.CustomReplace(oldCompanyName, oldProjectName, companyName, projectName);

            // 文件名包含模板关键字
            if (fileInfo.Name.Contains(oldCompanyName)
                || fileInfo.Name.Contains(oldProjectName))
            {
                var oldFileName = fileInfo.Name;
                var newFileName = oldFileName.CustomReplace(oldCompanyName, oldProjectName, companyName, projectName);

                var newFilePath = Path.Combine(fileInfo.DirectoryName, newFileName);
                // 无变化才重命名
                if (newFilePath != fileInfo.FullName)
                {
                    File.Delete(fileInfo.FullName);
                }

                File.WriteAllText(newFilePath, newContents, encoding);
            }
            else
                File.WriteAllText(fileInfo.FullName, newContents, encoding);
        }

        foreach (var subDirectory in Directory.GetDirectories(sourcePath))
        {
            RenameAllFileNameAndContent(subDirectory, oldCompanyName, oldProjectName, companyName, projectName, replaceSuffix);
        }
    }

    private static void RenameAllFileNameAndContent(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName,
        string replaceSuffix)
    {
        var list = new DirectoryInfo(sourcePath)
            .GetFiles()
            .Where(f => replaceSuffix.Contains(f.Extension))
            .ToList();

        var encoding = new UTF8Encoding(false);
        foreach (var fileInfo in list)
        {
            // 改文件内容
            var oldContents = File.ReadAllText(fileInfo.FullName, encoding);
            var newContents = oldContents.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName);

            // 文件名包含模板关键字
            if (fileInfo.Name.Contains(oldCompanyName)
                || fileInfo.Name.Contains(oldProjectName)
                || fileInfo.Name.Contains(oldModuleName))
            {
                var oldFileName = fileInfo.Name;
                var newFileName = oldFileName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName);

                var newFilePath = Path.Combine(fileInfo.DirectoryName, newFileName);
                // 无变化才重命名
                if (newFilePath != fileInfo.FullName)
                {
                    File.Delete(fileInfo.FullName);
                }

                File.WriteAllText(newFilePath, newContents, encoding);
            }
            else
                File.WriteAllText(fileInfo.FullName, newContents, encoding);
        }

        foreach (var subDirectory in Directory.GetDirectories(sourcePath))
        {
            RenameAllFileNameAndContent(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix);
        }
    }

    private static string CustomReplace(this string content, string oldCompanyName, string oldProjectName, string companyName, string projectName)
    {
        var result = content.ReplacePackageReferenceBasicManagement();
        content.ReplacePackageReferenceLanguageManagement();
        content.ReplacePackageReferenceFileManagement();
        content.ReplacePackageReferenceDataDictionaryManagement();
        content.ReplacePackageReferenceNotificationManagement();
        content.ReplacePackageReferenceCore();

        result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
            ;

        return result;
    }

    private static string CustomReplace(this string content, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName)
    {
        var result = content.ReplacePackageReferenceBasicManagement();
        content.ReplacePackageReferenceLanguageManagement();
        content.ReplacePackageReferenceFileManagement();
        content.ReplacePackageReferenceDataDictionaryManagement();
        content.ReplacePackageReferenceNotificationManagement();
        content.ReplacePackageReferenceCore();

        result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
                .Replace(oldModuleName, moduleName)
            ;

        return result;
    }

    public static string ReplacePackageReferenceCore(this string content)
    {
        return content.Replace("..\\..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj", "Lion.AbpPro.Core")
            .Replace("..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj", "Lion.AbpPro.Core")
            .Replace("..\\..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Microservices\\Lion.AbpPro.Shared.Hosting.Microservices.csproj",
                "Lion.AbpPro.Shared.Hosting.Microservices")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Microservices\\Lion.AbpPro.Shared.Hosting.Microservices.csproj", "Lion.AbpPro.Shared.Hosting.Microservices")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Gateways\\Lion.AbpPro.Shared.Hosting.Gateways.csproj", "Lion.AbpPro.Shared.Hosting.Gateways");
    }

    public static string ReplacePackageReferenceBasicManagement(this string content)
    {
        return content.Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application\\Lion.AbpPro.BasicManagement.Application.csproj",
                "Lion.AbpPro.BasicManagement.Application")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application.Contracts\\Lion.AbpPro.BasicManagement.Application.Contracts.csproj",
                "Lion.AbpPro.BasicManagement.Application.Contracts")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain\\Lion.AbpPro.BasicManagement.Domain.csproj",
                "Lion.AbpPro.BasicManagement.Domain")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain.Shared\\Lion.AbpPro.BasicManagement.Domain.Shared.csproj",
                "Lion.AbpPro.BasicManagement.Domain.Shared")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.EntityFrameworkCore\\Lion.AbpPro.BasicManagement.EntityFrameworkCore.csproj",
                "Lion.AbpPro.BasicManagement.EntityFrameworkCore")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.FreeSqlRepository\\Lion.AbpPro.BasicManagement.FreeSqlRepository.csproj",
                "Lion.AbpPro.BasicManagement.FreeSqlRepository")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi\\Lion.AbpPro.BasicManagement.HttpApi.csproj",
                "Lion.AbpPro.BasicManagement.HttpApi")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi.Client\\Lion.AbpPro.BasicManagement.HttpApi.Client.csproj",
                "Lion.AbpPro.BasicManagement.HttpApi.Client");
    }

    public static string ReplacePackageReferenceDataDictionaryManagement(this string content)
    {
        return content.Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application\\Lion.AbpPro.DataDictionaryManagement.Application.csproj",
                "Lion.AbpPro.DataDictionaryManagement.Application")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts.csproj",
                "Lion.AbpPro.DataDictionaryManagement.Application.Contracts")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain\\Lion.AbpPro.DataDictionaryManagement.Domain.csproj",
                "Lion.AbpPro.DataDictionaryManagement.Domain")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared.csproj",
                "Lion.AbpPro.DataDictionaryManagement.Domain.Shared")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.csproj",
                "Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository.csproj",
                "Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi\\Lion.AbpPro.DataDictionaryManagement.HttpApi.csproj",
                "Lion.AbpPro.DataDictionaryManagement.HttpApi")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client.csproj",
                "Lion.AbpPro.DataDictionaryManagement.HttpApi.Client");
    }

    public static string ReplacePackageReferenceFileManagement(this string content)
    {
        return content.Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application\\Lion.AbpPro.FileManagement.Application.csproj",
                "Lion.AbpPro.FileManagement.Application")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application.Contracts\\Lion.AbpPro.FileManagement.Application.Contracts.csproj",
                "Lion.AbpPro.FileManagement.Application.Contracts")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain\\Lion.AbpPro.FileManagement.Domain.csproj",
                "Lion.AbpPro.FileManagement.Domain")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain.Shared\\Lion.AbpPro.FileManagement.Domain.Shared.csproj",
                "Lion.AbpPro.FileManagement.Domain.Shared")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.EntityFrameworkCore\\Lion.AbpPro.FileManagement.EntityFrameworkCore.csproj",
                "Lion.AbpPro.FileManagement.EntityFrameworkCore")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.FreeSqlRepository\\Lion.AbpPro.FileManagement.FreeSqlRepository.csproj",
                "Lion.AbpPro.FileManagement.FreeSqlRepository")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi\\Lion.AbpPro.FileManagement.HttpApi.csproj",
                "Lion.AbpPro.FileManagement.HttpApi")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi.Client\\Lion.AbpPro.FileManagement.HttpApi.Client.csproj",
                "Lion.AbpPro.FileManagement.HttpApi.Client");
    }

    public static string ReplacePackageReferenceLanguageManagement(this string content)
    {
        return content.Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application\\Lion.AbpPro.LanguageManagement.Application.csproj",
                "Lion.AbpPro.LanguageManagement.Application")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application.Contracts\\Lion.AbpPro.LanguageManagement.Application.Contracts.csproj",
                "Lion.AbpPro.LanguageManagement.Application.Contracts")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain\\Lion.AbpPro.LanguageManagement.Domain.csproj",
                "Lion.AbpPro.LanguageManagement.Domain")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain.Shared\\Lion.AbpPro.LanguageManagement.Domain.Shared.csproj",
                "Lion.AbpPro.LanguageManagement.Domain.Shared")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore.csproj",
                "Lion.AbpPro.LanguageManagement.EntityFrameworkCore")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.FreeSqlRepository\\Lion.AbpPro.LanguageManagement.FreeSqlRepository.csproj",
                "Lion.AbpPro.LanguageManagement.FreeSqlRepository")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi\\Lion.AbpPro.LanguageManagement.HttpApi.csproj",
                "Lion.AbpPro.LanguageManagement.HttpApi")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi.Client\\Lion.AbpPro.LanguageManagement.HttpApi.Client.csproj",
                "Lion.AbpPro.LanguageManagement.HttpApi.Client");
    }

    public static string ReplacePackageReferenceNotificationManagement(this string content)
    {
        return content.Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application\\Lion.AbpPro.NotificationManagement.Application.csproj",
                "Lion.AbpPro.NotificationManagement.Application")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application.Contracts\\Lion.AbpPro.NotificationManagement.Application.Contracts.csproj",
                "Lion.AbpPro.NotificationManagement.Application.Contracts")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain\\Lion.AbpPro.NotificationManagement.Domain.csproj",
                "Lion.AbpPro.NotificationManagement.Domain")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain.Shared\\Lion.AbpPro.NotificationManagement.Domain.Shared.csproj",
                "Lion.AbpPro.NotificationManagement.Domain.Shared")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore.csproj",
                "Lion.AbpPro.NotificationManagement.EntityFrameworkCore")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.FreeSqlRepository\\Lion.AbpPro.NotificationManagement.FreeSqlRepository.csproj",
                "Lion.AbpPro.NotificationManagement.FreeSqlRepository")
            .Replace("..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi\\Lion.AbpPro.NotificationManagement.HttpApi.csproj",
                "Lion.AbpPro.NotificationManagement.HttpApi")
            .Replace(
                "..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi.Client\\Lion.AbpPro.NotificationManagement.HttpApi.Client.csproj",
                "Lion.AbpPro.NotificationManagement.HttpApi.Client");
    }
}