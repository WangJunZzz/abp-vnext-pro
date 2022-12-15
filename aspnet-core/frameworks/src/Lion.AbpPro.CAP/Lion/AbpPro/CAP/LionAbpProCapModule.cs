namespace Lion.AbpPro.CAP;

[DependsOn(
    typeof(AbpEventBusModule), 
    typeof(LionAbpProLocalizationModule))]
public class LionAbpProCapModule : AbpModule
{
}