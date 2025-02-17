namespace Lion.AbpPro.Cli.Utils;

public static class ZipHelper
{
    public static string Extract(string zipPath, string repositoryId, string version)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
        {
            throw new ArgumentNullException(nameof(zipPath));
        }

        var targetPath = Path.Combine(Path.GetDirectoryName(zipPath) ?? string.Empty, repositoryId + "-" + version);

        if (Directory.Exists(targetPath)) return Path.Combine(targetPath, repositoryId);

        System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, targetPath, Encoding.UTF8, true);

        var directoryName = Path.Combine(targetPath, Directory.GetDirectories(targetPath).First());
        Directory.Move(directoryName, Path.Combine(targetPath, repositoryId));

        return Path.Combine(targetPath, repositoryId);
    }

    public static string Extract(string zipPath, string targetPath = "")
    {
        if (zipPath.IsNullOrWhiteSpace())
        {
            throw new ArgumentNullException(nameof(zipPath));
        }

        if (targetPath.IsNullOrWhiteSpace())
        {
            // 获取不包含扩展名的文件名
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipPath);
            // 获取文件所在目录的路径
            var directoryPath = Path.GetDirectoryName(zipPath);
            // 组合目录路径和不包含扩展名的文件名
            targetPath = Path.Combine(directoryPath, fileNameWithoutExtension);
        }

        if (Directory.Exists(targetPath))
        {
            DirectoryHelper.DeletedDir(targetPath);
        }

        System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, targetPath, Encoding.UTF8, true);

        return targetPath;
    }
}