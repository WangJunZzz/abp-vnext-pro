namespace Lion.AbpPro.Cli;

public class AbpCliOptions
{
    public Dictionary<string, Type> Commands { get; }


    /// <summary>
    /// Default value: "lion.abp".
    /// </summary>
    public string ToolName { get; set; } = "lion.abp";

    public AbpCliOptions()
    {
        Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
    }
}