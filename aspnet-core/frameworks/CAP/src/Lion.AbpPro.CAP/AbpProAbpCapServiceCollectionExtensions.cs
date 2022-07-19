namespace Lion.AbpPro.CAP
{
    public static class AbpProAbpCapServiceCollectionExtensions
    {
        public static ServiceConfigurationContext AddAbpCap(
            this ServiceConfigurationContext context, 
            Action<CapOptions> capAction)
        {
            context.Services.AddCap(capAction);
            context.Services.AddSingleton<IConsumerServiceSelector, AbpProAbpCapConsumerServiceSelector>();
            context.Services.AddSingleton<IDistributedEventBus, AbpProAbpCapDistributedEventBus>();
            return context;
        }
    }
}
