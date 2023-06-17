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
        var result = content.ReplacePackageReferenceBasicManagement()
            .ReplacePackageReferenceLanguageManagement()
            .ReplacePackageReferenceFileManagement()
            .ReplacePackageReferenceDataDictionaryManagement()
            .ReplacePackageReferenceNotificationManagement()
            .ReplacePackageReferenceCore();

        result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
            ;

        return result;
    }

    private static string CustomReplace(this string content, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName)
    {
        var result = content.ReplacePackageReferenceBasicManagement()
            .ReplacePackageReferenceLanguageManagement()
            .ReplacePackageReferenceFileManagement()
            .ReplacePackageReferenceDataDictionaryManagement()
            .ReplacePackageReferenceNotificationManagement()
            .ReplacePackageReferenceCore();

        result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
                .Replace(oldModuleName, moduleName)
            ;

        return result;
    }

    public static string ReplacePackageReferenceCore(this string content)
    {
       
        return content
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Microservices\\Lion.AbpPro.Shared.Hosting.Microservices.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Microservices\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Gateways\\Lion.AbpPro.Shared.Hosting.Gateways.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Gateways\"/>")
            ;
    }

    public static string ReplacePackageReferenceBasicManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application\\Lion.AbpPro.BasicManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application.Contracts\\Lion.AbpPro.BasicManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain\\Lion.AbpPro.BasicManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain.Shared\\Lion.AbpPro.BasicManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.EntityFrameworkCore\\Lion.AbpPro.BasicManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.FreeSqlRepository\\Lion.AbpPro.BasicManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi\\Lion.AbpPro.BasicManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi.Client\\Lion.AbpPro.BasicManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceDataDictionaryManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application\\Lion.AbpPro.DataDictionaryManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain\\Lion.AbpPro.DataDictionaryManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi\\Lion.AbpPro.DataDictionaryManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceFileManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application\\Lion.AbpPro.FileManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application.Contracts\\Lion.AbpPro.FileManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain\\Lion.AbpPro.FileManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain.Shared\\Lion.AbpPro.FileManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.EntityFrameworkCore\\Lion.AbpPro.FileManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.FreeSqlRepository\\Lion.AbpPro.FileManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi\\Lion.AbpPro.FileManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi.Client\\Lion.AbpPro.FileManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceLanguageManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application\\Lion.AbpPro.LanguageManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application.Contracts\\Lion.AbpPro.LanguageManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain\\Lion.AbpPro.LanguageManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain.Shared\\Lion.AbpPro.LanguageManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.FreeSqlRepository\\Lion.AbpPro.LanguageManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi\\Lion.AbpPro.LanguageManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi.Client\\Lion.AbpPro.LanguageManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceNotificationManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application\\Lion.AbpPro.NotificationManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application.Contracts\\Lion.AbpPro.NotificationManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain\\Lion.AbpPro.NotificationManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain.Shared\\Lion.AbpPro.NotificationManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.FreeSqlRepository\\Lion.AbpPro.NotificationManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi\\Lion.AbpPro.NotificationManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi.Client\\Lion.AbpPro.NotificationManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi.Client\"/>");
    }
}