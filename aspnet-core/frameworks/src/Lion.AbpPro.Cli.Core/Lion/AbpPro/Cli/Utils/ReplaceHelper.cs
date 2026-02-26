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
        string version,
        bool vben5)
    {
        try
        {
            RenameTemplate(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version, vben5);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"生成模板失败{ex.Message};{ex.StackTrace}");
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
        string version,
        bool vben5)
    {
        RenameAllDirectories(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5);
        RenameAllFileNameAndContent(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version, vben5);
    }

    private static void RenameAllDirectories(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string version,
        bool vben5)
    {
        // 先收集所有需要重命名的目录，按深度排序（深的先处理）
        var directoriesToRename = new List<(DirectoryInfo DirectoryInfo, string NewDirectoryPath)>();
        CollectDirectoriesToRename(sourcePath, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5, directoriesToRename);

        // 按路径深度降序排序，确保先处理深层目录，再处理父目录
        var sortedDirectories = directoriesToRename
            .OrderByDescending(d => d.DirectoryInfo.FullName.Count(c => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
            .ToList();

        foreach (var (directoryInfo, newDirectoryPath) in sortedDirectories)
        {
            if (directoryInfo.FullName != newDirectoryPath)
            {
                try
                {
                    directoryInfo.MoveTo(newDirectoryPath);
                }
                catch (IOException ex)
                {
                    // 如果目录被占用，等待重试
                    Thread.Sleep(100);
                    directoryInfo.MoveTo(newDirectoryPath);
                }
            }
        }
    }

    private static void CollectDirectoriesToRename(
        string sourcePath,
        string oldCompanyName,
        string oldProjectName,
        string oldModuleName,
        string companyName,
        string projectName,
        string moduleName,
        string version,
        bool vben5,
        List<(DirectoryInfo DirectoryInfo, string NewDirectoryPath)> directoriesToRename)
    {
        var directories = Directory.GetDirectories(sourcePath);
        foreach (var subDirectory in directories)
        {
            var directoryInfo = new DirectoryInfo(subDirectory);

            // 先递归收集子目录
            CollectDirectoriesToRename(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5, directoriesToRename);

            // 检查当前目录是否需要重命名
            if (directoryInfo.Name.Contains(oldCompanyName) ||
                directoryInfo.Name.Contains(oldProjectName) ||
                directoryInfo.Name.Contains(oldModuleName))
            {
                var oldDirectoryName = directoryInfo.Name;
                var newDirectoryName = oldDirectoryName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5);
                var newDirectoryPath = Path.Combine(directoryInfo.Parent?.FullName, newDirectoryName);

                directoriesToRename.Add((directoryInfo, newDirectoryPath));
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
        string version,
        bool vben5)
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
            var newContents = oldContents.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5);

            // 文件名包含模板关键字
            if (fileInfo.Name.Contains(oldCompanyName)
                || fileInfo.Name.Contains(oldProjectName)
                || fileInfo.Name.Contains(oldModuleName))
            {
                var oldFileName = fileInfo.Name;
                var newFileName = oldFileName.CustomReplace(oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, version, vben5);

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
            RenameAllFileNameAndContent(subDirectory, oldCompanyName, oldProjectName, oldModuleName, companyName, projectName, moduleName, replaceSuffix, version, vben5);
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
        string version,
        bool vben5)
    {
        string result;
        if (!vben5)
        {
            result = content.ReplacePackageReferenceBasicManagement()
                .ReplacePackageReferenceLanguageManagement()
                .ReplacePackageReferenceFileManagement()
                .ReplacePackageReferenceDataDictionaryManagement()
                .ReplacePackageReferenceNotificationManagement()
                .ReplacePackageReferenceCore()
                .ReplaceLionPackageVersion(version);
        }
        else
        {
            result = content.Vben5ReplacePackageReferenceBasicManagement()
                .Vben5ReplacePackageReferenceLanguageManagement()
                .Vben5ReplacePackageReferenceFileManagement()
                .Vben5ReplacePackageReferenceDataDictionaryManagement()
                .Vben5ReplacePackageReferenceNotificationManagement()
                .Vben5ReplacePackageReferenceCodeManagement()
                .Vben5ReplacePackageReferenceTemplateManagement()
                .Vben5ReplacePackageReferenceFileManagement()
                .Vben5ReplacePackageReferenceImportExportManagement()
                .Vben5ReplacePackageReferenceDynamicMenuManagement()
                .Vben5ReplacePackageReferenceCacheManagement()
                .Vben5ReplacePackageReferenceMasterDataManagement()
                .Vben5ReplacePackageReferenceCore()
                .Vben5ReplaceLionPackageVersion(version);
        }


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