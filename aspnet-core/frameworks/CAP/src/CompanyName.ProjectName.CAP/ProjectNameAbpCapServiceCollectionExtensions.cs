using System;
using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.CAP
{
    public static class ProjectNameAbpCapServiceCollectionExtensions
    {
        public static ServiceConfigurationContext AddAbpCap(
            this ServiceConfigurationContext context, 
            Action<CapOptions> capAction)
        {
            context.Services.AddCap(capAction);
            context.Services.AddSingleton<IConsumerServiceSelector, ProjectNameAbpCapConsumerServiceSelector>();
            context.Services.AddSingleton<IDistributedEventBus, ProjectNameAbpCapDistributedEventBus>();
            return context;
        }
    }
}
