namespace Lion.AbpPro.Cli.Utils;

public static class ProcessHelper
{
    public static void OpenExplorer(string path)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("explorer.exe", path);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS 系统使用 open 命令打开文件夹
                var startInfo = new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = path,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
            }
        }
        catch (Exception e)
        {
            // ignore
        }
    }
}