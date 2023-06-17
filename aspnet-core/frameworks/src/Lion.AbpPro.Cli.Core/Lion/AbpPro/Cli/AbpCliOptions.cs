namespace Lion.AbpPro.Cli;

public class AbpCliOptions
{
    public Dictionary<string, Type> Commands { get; }


    /// <summary>
    /// Default value: "abppro".
    /// </summary>
    public string ToolName { get; set; } = "abppro";

    public AbpCliOptions()
    {
        Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
    }
}