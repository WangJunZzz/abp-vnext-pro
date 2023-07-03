namespace Lion.AbpPro.Cli.Args;

public class CommandLineArgs
{
    public string Command { get; }

    public string Target { get; }


    public AbpCommandLineOptions Options { get; }

    public CommandLineArgs(string command = null, string target = null)
    {
        Command = command;
        Target = target;
        Options = new AbpCommandLineOptions();
    }

    public static CommandLineArgs Empty()
    {
        return new CommandLineArgs();
    }

    public bool IsCommand(string command)
    {
        return string.Equals(Command, command, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Command != null)
        {
            sb.AppendLine($"Command: {Command}");
        }

        if (Target != null)
        {
            sb.AppendLine($"Target: {Target}");
        }

        if (Options.Any())
        {
            sb.AppendLine("Options:");
            foreach (var option in Options)
            {
                sb.AppendLine($" - {option.Key} = {option.Value}");
            }
        }

        if (sb.Length <= 0)
        {
            sb.Append("<EMPTY>");
        }

        return sb.ToString();
    }
}