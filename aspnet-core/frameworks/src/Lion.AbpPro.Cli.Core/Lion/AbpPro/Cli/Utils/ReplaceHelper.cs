using Volo.Abp.Domain.Services;

namespace Lion.AbpPro.Cli.Replace;

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

    private static void ReplaceTemplates(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName, string replaceSuffix)
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

    private static void RenameTemplate(string sourcePath, string oldCompanyName, string oldProjectName, string oldModuleName, string companyName, string projectName, string moduleName, string replaceSuffix)
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
    private static string CustomReplace(this string content,string oldCompanyName, string oldProjectName, string companyName,string projectName)
    {
        var result = content
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
            ;

        return result;
    }
    
    private static string CustomReplace(this string content,string oldCompanyName, string oldProjectName,string oldModuleName, string companyName,string projectName,string moduleName)
    {
        var result = content
                .Replace(oldCompanyName, companyName)
                .Replace(oldProjectName, projectName)
                .Replace(oldModuleName,moduleName)
            ;

        return result;
    }
}