namespace Lion.AbpPro.Cli;

public class CliPaths
{
    public static string Log => Path.Combine(AbpRootPath, "logs");
    
    public static string TemplateCache => Path.Combine(AbpRootPath, "templates"); 
    
    public static readonly string AbpRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".abp.pro");
}