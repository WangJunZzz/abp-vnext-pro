using DotNetCore.CAP.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lion.AbpPro.CAP;

public static class LionAbpProCapServiceCollectionExtensions
{
    public static ServiceConfigurationContext AddAbpCap(
        this ServiceConfigurationContext context, 
        Action<CapOptions> capAction)
    {
        // context.Services.AddSingleton<IConsumerServiceSelector, LionAbpProCapConsumerServiceSelector>();
        // context.Services.AddSingleton<IDistributedEventBus, LionAbpProCapDistributedEventBus>();
        // context.Services.AddSingleton<ISerializer, LionAbpProJsonSerializer>();
        
        context.Services.Replace(ServiceDescriptor.Transient<IUnitOfWork, LionAbpProCapUnitOfWork>());
        context.Services.Replace(ServiceDescriptor.Transient<UnitOfWork, LionAbpProCapUnitOfWork>());
        context.Services.AddTransient<LionAbpProCapUnitOfWork>();
        
        context.Services.AddCap(capAction);
        return context;
    }
}