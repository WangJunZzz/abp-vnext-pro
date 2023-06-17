namespace Lion.AbpPro.Cli.Args;

public interface ICommandLineArgumentParser
{
    CommandLineArgs Parse(string[] args);

    CommandLineArgs Parse(string lineText);
}