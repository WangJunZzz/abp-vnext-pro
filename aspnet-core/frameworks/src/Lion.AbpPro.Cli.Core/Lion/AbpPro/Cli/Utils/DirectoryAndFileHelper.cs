using Volo.Abp.Domain.Services;

namespace Lion.AbpPro.Cli.Zip;

public static class DirectoryAndFileHelper 
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
        catch
        {
            throw new UserFriendlyException("复制文件失败！");
        }
    }

    /// <summary>
    /// 删除文件夹及文件
    /// </summary>
    /// <param name="srcPath">路径</param>
    private static void DeletedDir(string srcPath)
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
}