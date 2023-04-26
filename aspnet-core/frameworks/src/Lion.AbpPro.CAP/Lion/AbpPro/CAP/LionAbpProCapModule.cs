namespace Lion.AbpPro.CAP;

[DependsOn(
    typeof(AbpEventBusModule), 
    typeof(LionAbpProLocalizationModule),
    typeof(AbpUnitOfWorkModule))]
public class LionAbpProCapModule : AbpModule
{
}