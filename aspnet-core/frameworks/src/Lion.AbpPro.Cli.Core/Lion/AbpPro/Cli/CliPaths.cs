namespace Lion.AbpPro.Cli;

public class CliPaths
{
    public static string Log => Path.Combine(AbpRootPath, "logs");
    public static string Root => Path.Combine(AbpRootPath, "cli");

    public static string AccessToken => Path.Combine(AbpRootPath, "cli", "access-token.bin");
    public static string Output => Path.Combine(AbpRootPath, "cli", "code", "output");
    public static string Source => Path.Combine(AbpRootPath, "cli", "code", "source");

    public static string TemplateCache => Path.Combine(AbpRootPath, "templates");


    public static readonly string AbpRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".abp.pro");
}