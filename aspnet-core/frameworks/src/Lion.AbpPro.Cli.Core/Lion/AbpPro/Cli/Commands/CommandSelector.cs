namespace Lion.AbpPro.Cli.Commands;

public class CommandSelector : ICommandSelector, ITransientDependency
{
    private readonly AbpCliOptions _options;

    public CommandSelector(IOptions<AbpCliOptions> options)
    {
        _options = options.Value;
    }

    public Type Select(CommandLineArgs commandLineArgs)
    {
        if (commandLineArgs.Command.IsNullOrWhiteSpace())
        {
            return typeof(HelpCommand);
        }

        return _options.Commands.GetOrDefault(commandLineArgs.Command)
               ?? typeof(HelpCommand);
    }
}