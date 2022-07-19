namespace Lion.AbpPro.CAP
{
    [Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
    [ExposeServices(typeof(IConsumerServiceSelector))]
    public sealed class AbpProAbpCapConsumerServiceSelector : ConsumerServiceSelector
    {
        private AbpDistributedEventBusOptions AbpDistributedEventBusOptions { get; }
        private IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Creates a new <see cref="T:DotNetCore.CAP.Internal.ConsumerServiceSelector" />.
        /// </summary>
        public AbpProAbpCapConsumerServiceSelector(
            IServiceProvider serviceProvider, 
            IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions) 
            : base(serviceProvider)
        {
            ServiceProvider = serviceProvider;
            AbpDistributedEventBusOptions = distributedEventBusOptions.Value;
        }

        protected override IEnumerable<ConsumerExecutorDescriptor> FindConsumersFromInterfaceTypes(IServiceProvider provider)
        {
            var executorDescriptorList = base.FindConsumersFromInterfaceTypes(provider).ToList();
            
            //handlers
            var handlers = AbpDistributedEventBusOptions.Handlers;

            foreach (var handler in handlers)
            {
                var interfaces = handler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                    {
                        continue;
                    }
                    var genericArgs = @interface.GetGenericArguments();
                    
                    if (genericArgs.Length != 1)
                    {
                        continue;
                    }

                    var descriptors = GetHandlerDescription(genericArgs[0], handler);

                    foreach (var descriptor in descriptors)
                    {
                        var count = executorDescriptorList.Count(x =>
                            x.Attribute.Name == descriptor.Attribute.Name);

                        descriptor.Attribute.Group = descriptor.Attribute.Group.Insert(
                            descriptor.Attribute.Group.LastIndexOf(".", StringComparison.Ordinal), $".{count}");
                            
                        executorDescriptorList.Add(descriptor);
                    }
                        
                    //Subscribe(genericArgs[0], new IocEventHandlerFactory(ServiceScopeFactory, handler));
                }
            }
            return executorDescriptorList;
        }

        private IEnumerable<ConsumerExecutorDescriptor> GetHandlerDescription(Type eventType,Type typeInfo)
        {
            var serviceTypeInfo = typeof(IDistributedEventHandler<>)
                .MakeGenericType(eventType);
            var method = typeInfo
                .GetMethod(
                    nameof(IDistributedEventHandler<object>.HandleEventAsync),
                    new[] { eventType }
                );
            var eventName = EventNameAttribute.GetNameOrDefault(eventType);
            var topicAttr = method.GetCustomAttributes<TopicAttribute>(true);
            var topicAttributes = topicAttr.ToList();

            if (topicAttributes.Count == 0)
            {
                topicAttributes.Add(new CapSubscribeAttribute(eventName));
            }

            foreach (var attr in topicAttributes)
            {
                SetSubscribeAttribute(attr);

                var parameters = method.GetParameters()
                    .Select(parameter => new ParameterDescriptor
                    {
                        Name = parameter.Name,
                        ParameterType = parameter.ParameterType,
                        IsFromCap = parameter.GetCustomAttributes(typeof(FromCapAttribute)).Any()
                    }).ToList();

                yield return InitDescriptor(attr, method, typeInfo.GetTypeInfo(), serviceTypeInfo.GetTypeInfo(), parameters);
            }
        }

        private static ConsumerExecutorDescriptor InitDescriptor(
            TopicAttribute attr,
            MethodInfo methodInfo,
            TypeInfo implType,
            TypeInfo serviceTypeInfo,
            IList<ParameterDescriptor> parameters)
        {
            var descriptor = new ConsumerExecutorDescriptor
            {
                Attribute = attr,
                MethodInfo = methodInfo,
                ImplTypeInfo = implType,
                ServiceTypeInfo = serviceTypeInfo,
                Parameters = parameters
            };

            return descriptor;
        }
    }
}