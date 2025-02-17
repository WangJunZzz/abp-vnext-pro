namespace Lion.AbpPro.Cli.Utils;

public static class DirectoryHelper
{
    /// <summary>
    /// 复制文件夹及文件
    /// </summary>
    /// <param name="sourceFolder">原文件路径</param>
    /// <param name="destFolder">目标文件路径</param>
    /// <param name="excludeFiles">需要排除的文件，多个文件用逗号隔开</param>
    public static void CopyFolder(string sourceFolder, string destFolder, string excludeFiles = "")
    {
        try
        {
            Check.NotNullOrWhiteSpace(sourceFolder, nameof(sourceFolder));
            Check.NotNullOrWhiteSpace(destFolder, nameof(destFolder));
            //如果目标路径不存在,则创建目标路径
            if (!System.IO.Directory.Exists(destFolder))
            {
                System.IO.Directory.CreateDirectory(destFolder);
            }
            else
            {
                DeletedDir(destFolder);
            }

            //得到原文件根目录下的所有文件
            string[] files = System.IO.Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = System.IO.Path.GetFileName(file);
                string dest = System.IO.Path.Combine(destFolder, name);
                if (!excludeFiles.IsNullOrWhiteSpace())
                {
                    var excludes = excludeFiles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (excludes.Contains(name))
                    {
                        continue;
                    }
                }

                System.IO.File.Copy(file, dest); //复制文件
            }

            //得到原文件根目录下的所有文件夹
            string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                if (!excludeFiles.IsNullOrWhiteSpace())
                {
                    var excludes = excludeFiles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (excludes.Contains(name))
                    {
                        continue;
                    }
                }


                CopyFolder(folder, dest); //构建目标路径,递归复制文件
            }
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("复制文件失败！" + ex.Message + ex.StackTrace);
        }
    }

    /// <summary>
    /// 删除文件夹及文件
    /// </summary>
    /// <param name="srcPath">路径</param>
    public static void DeletedDir(string srcPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos(); //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo) //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true); //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName); //删除指定文件
                }
            }
        }
        catch (Exception)
        {
            throw new UserFriendlyException("删除文件失败！");
        }
    }

    /// <summary>
    /// 获取指定路径下所有扩展名为 .sln 的文件
    /// </summary>
    public static List<string> GetSolutionFiles(string path)
    {
        var solutionFiles = new List<string>();
        var files = Directory.GetFiles(path, "*.sln", SearchOption.AllDirectories);
        solutionFiles.AddRange(files);
        return solutionFiles;
    }

    public static void IsAbpProjectStructure(string path, string companyName, string projectName)
    {
        var dir = Directory.GetDirectories(path);

        if (
            // 判断是否有Application
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.Application") &&
            // 判断是否有Application.Contracts
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.Application.Contracts") &&
            // 判断是否有Domain
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.Domain") &&
            // 判断是否有Domain.Shared
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.Domain.Shared") &&
            // 判断是否有EntityFrameworkCore
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.EntityFrameworkCore") &&
            // 判断是否有HttpApi
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.HttpApi") &&
            // 判断是否有HttpApi.Client
            dir.Any(e => Path.GetFileName(e) == $"{companyName}.{projectName}.HttpApi.Client")
        )
        {
            return;
        }
        else
        {
            throw new UserFriendlyException($"请确认项目路径下的项目是否是abp标准结构:{path}");
        }
    }

    public static void IsVben5ProjectStructure(string path)
    {
        var dir = Directory.GetDirectories(path);

        if (
            // 判断是否有Application
            dir.Any(e => Path.GetFileName(e) == $"router") &&
            // 判断是否有Application.Contracts
            dir.Any(e => Path.GetFileName(e) == $"views")
        )
        {
            return;
        }
        else
        {
            throw new UserFriendlyException($"请确认项目路径下的项目是否是vben5标准结构:{path}");
        }
    }

    private static bool CheckFolder(string path, string name)
    {
        var dir = Directory.GetDirectories(path);

        // 检查路径是否指向一个文件夹
        if (Directory.Exists(path))
        {
            // 获取文件夹名
            var folderName = Path.GetFileName(path);
            // 判断文件夹名是否以 'Application' 结尾
            return folderName.EndsWith(name, StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }
}