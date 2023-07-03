namespace Lion.AbpPro.Cli;

[DependsOn(
    typeof(Lion.AbpPro.Cli.AbpProCliCoreModule),
    typeof(AbpAutofacModule)
)]
public class AbpProCliModule : AbpModule
{
}
