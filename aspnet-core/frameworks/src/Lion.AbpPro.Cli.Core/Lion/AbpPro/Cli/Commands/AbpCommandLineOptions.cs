namespace Lion.AbpPro.Cli.Commands;

public class AbpCommandLineOptions : Dictionary<string, string>
{
    public string GetOrNull(string name, params string[] alternativeNames)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var value = this.GetOrDefault(name);
        if (!value.IsNullOrWhiteSpace())
        {
            return value;
        }

        if (!alternativeNames.IsNullOrEmpty())
        {
            foreach (var alternativeName in alternativeNames)
            {
                value = this.GetOrDefault(alternativeName);
                if (!value.IsNullOrWhiteSpace())
                {
                    return value;
                }
            }
        }

        return null;
    }
}