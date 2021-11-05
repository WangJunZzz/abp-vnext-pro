using System;
using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;

namespace Lion.Abp.Cap
{
    public static class AbpCapServiceCollectionExtensions
    {
        public static ServiceConfigurationContext AddAbpCap(
            this ServiceConfigurationContext context, 
            Action<CapOptions> capAction)
        {
            context.Services.AddCap(capAction);
            context.Services.AddSingleton<IConsumerServiceSelector, LionAbpCapConsumerServiceSelector>();
            context.Services.AddSingleton<IDistributedEventBus, LionAbpCapDistributedEventBus>();
            return context;
        }
    }
}
