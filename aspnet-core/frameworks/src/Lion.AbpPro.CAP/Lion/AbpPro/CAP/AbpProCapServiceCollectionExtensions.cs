namespace Lion.AbpPro.CAP;

public static class AbpProCapServiceCollectionExtensions
{
    public static ServiceConfigurationContext AddAbpCap(this ServiceConfigurationContext context, Action<CapOptions> capAction)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IUnitOfWork, AbpProCapUnitOfWork>());
        context.Services.Replace(ServiceDescriptor.Transient<UnitOfWork, AbpProCapUnitOfWork>());
        context.Services.AddTransient<AbpProCapUnitOfWork>();
        context.Services.AddCap(capAction);
        return context;
    }
}