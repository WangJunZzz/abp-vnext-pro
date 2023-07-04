namespace Lion.AbpPro.Cli.Utils;

public static class ReplaceHelper
{
    public static void ReplaceTemplates(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string replaceSuffix,
        string version)
    {
        try
        {
            RenameTemplate(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"生成模板失败{ex.Message}");
        }
    }

    private static void RenameTemplate(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string replaceSuffix,
        string version)
    {
        RenameAllDirectories(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version);
        RenameAllFileNameAndContent(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version);
    }

    private static void RenameAllDirectories(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string version)
    {
        var directories = Directory.GetDirectories(sourcePath);
        foreach (var subDirectory in directories)
        {
            RenameAllDirectories(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version);

            var directoryInfo = new DirectoryInfo(subDirectory);
            if (directoryInfo.Name.Contains(oldCompanyName) ||
                directoryInfo.Name.Contains(oldProjectName) ||
                directoryInfo.Name.Contains(oldModuleName))
            {
                var oldDirectoryName = directoryInfo.Name;
                var newDirectoryName = oldDirectoryName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version);

                var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName, newDirectoryName);

                if (directoryInfo.FullName != newDirectoryPath)
                {
                    directoryInfo.MoveTo(newDirectoryPath);
                }
            }
        }
    }


    private static void RenameAllFileNameAndContent(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string replaceSuffix,
        string version)
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
            var newContents = oldContents.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version);

            // 文件名包含模板关键字
            if (fileInfo.Name.Contains(oldCompanyName)
                || fileInfo.Name.Contains(oldProjectName)
                || fileInfo.Name.Contains(oldModuleName))
            {
                var oldFileName = fileInfo.Name;
                var newFileName = oldFileName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version);

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
            RenameAllFileNameAndContent(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version);
        }
    }


    private static string CustomReplace(
        this string content,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string version)
    {
        var result = content.ReplacePackageReferenceBasicManagement()
            .ReplacePackageReferenceLanguageManagement()
            .ReplacePackageReferenceFileManagement()
            .ReplacePackageReferenceDataDictionaryManagement()
            .ReplacePackageReferenceNotificationManagement()
            .ReplacePackageReferenceCore()
            .ReplaceLionPackageVersion(version);

        if (oldModuleName.IsNullOrWhiteSpace() || oldModuleName.IsNullOrWhiteSpace())
        {
            result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName);
        }
        else
        {
            result = result
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
                .Replace(oldModuleName, moduleName);
        }

        return result;
    }
}