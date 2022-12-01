namespace Lion.AbpPro.CAP;

public static class LionAbpProCapServiceCollectionExtensions
{
    public static ServiceConfigurationContext AddAbpCap(
        this ServiceConfigurationContext context, 
        Action<CapOptions> capAction)
    {
        context.Services.AddCap(capAction);
        context.Services.AddSingleton<IConsumerServiceSelector, LionAbpProCapConsumerServiceSelector>();
        context.Services.AddSingleton<IDistributedEventBus, LionAbpProCapDistributedEventBus>();
        return context;
    }
}