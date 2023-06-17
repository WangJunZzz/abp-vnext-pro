namespace Lion.AbpPro.Cli.Commands;

public interface ICommandSelector
{
    Type Select(CommandLineArgs commandLineArgs);
}