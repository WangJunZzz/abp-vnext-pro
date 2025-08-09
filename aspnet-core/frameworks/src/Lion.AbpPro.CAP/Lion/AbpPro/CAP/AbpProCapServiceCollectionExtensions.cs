namespace Lion.AbpPro.CAP;

public static class AbpProCapServiceCollectionExtensions
{
    public static ServiceConfigurationContext AddAbpCap(this ServiceConfigurationContext context, Action<CapOptions> capAction)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IUnitOfWork, AbpProCapUnitOfWork>());
        context.Services.Replace(ServiceDescriptor.Transient<UnitOfWork, AbpProCapUnitOfWork>());
        context.Services.AddSingleton<ISubscribeInvoker, AbpProCAPSubscribeInvoker>();
        context.Services.AddTransient<AbpProCapUnitOfWork>();
        context.Services.AddCap(capAction);
        return context;
    }
    
    public static IServiceCollection AddAbpCap(this IServiceCollection service, Action<CapOptions> capAction)
    {
        service.Replace(ServiceDescriptor.Transient<IUnitOfWork, AbpProCapUnitOfWork>());
        service.Replace(ServiceDescriptor.Transient<UnitOfWork, AbpProCapUnitOfWork>());
        service.AddSingleton<ISubscribeInvoker, AbpProCAPSubscribeInvoker>();
        service.AddTransient<AbpProCapUnitOfWork>();
        service.AddCap(capAction);
        return service;
    }
}