namespace Lion.AbpPro.CAP
{
    public class AbpProAbpCapDistributedEventBus :
        EventBusBase,
        IDistributedEventBus,
        ISingletonDependency
    {
        private AbpDistributedEventBusOptions AbpDistributedEventBusOptions { get; }
        private ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }
        private ConcurrentDictionary<string, Type> EventTypes { get; }

        private readonly ICapPublisher CapPublisher;

        
        public AbpProAbpCapDistributedEventBus(IServiceScopeFactory serviceScopeFactory,
            IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions,
            ICapPublisher capPublisher,
            ICurrentTenant currentTenant,
            UnitOfWorkManager unitOfWorkManager,
            IEventHandlerInvoker eventHandlerInvoker)
            : base(serviceScopeFactory, currentTenant,unitOfWorkManager,eventHandlerInvoker)
        {
            CapPublisher = capPublisher;
            AbpDistributedEventBusOptions = distributedEventBusOptions.Value;
            HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
            EventTypes = new ConcurrentDictionary<string, Type>();
        }

        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            //This is handled by CAP ConsumerServiceSelector
            throw new NotImplementedException();
        }

        public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
        {
            Check.NotNull(action, nameof(action));

            GetOrCreateHandlerFactories(typeof(TEvent))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEvent>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        public override void Unsubscribe(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                    );
                });
        }

        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        public override void UnsubscribeAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }

        protected override Task PublishToEventBusAsync(Type eventType, object eventData)
        {
            throw new NotImplementedException();
        }

        protected override void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe<TEvent>(IDistributedEventHandler<TEvent> handler) where TEvent : class
        {
            return Subscribe(typeof(TEvent), handler);
        }

        public async Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true,
            bool useOutbox = true) where TEvent : class
        {
            var eventName = EventNameAttribute.GetNameOrDefault(typeof(TEvent));
            await CapPublisher.PublishAsync(eventName, eventData);

        }

        public async Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true,
            bool useOutbox = true)
        {
            var eventName = EventNameAttribute.GetNameOrDefault(eventType);
            await CapPublisher.PublishAsync(eventName, eventData);
        }
        

        protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in
                HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(
                    new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return HandlerFactories.GetOrAdd(
                eventType,
                type =>
                {
                    var eventName = EventNameAttribute.GetNameOrDefault(type);
                    EventTypes[eventName] = type;
                    return new List<IEventHandlerFactory>();
                }
            );
        }

        private static bool ShouldTriggerEventForHandler(Type targetEventType, Type handlerEventType)
        {
            //Should trigger same type
            if (handlerEventType == targetEventType)
            {
                return true;
            }

            //TODO: Support inheritance? But it does not support on subscription to RabbitMq!
            //Should trigger for inherited types
            if (handlerEventType.IsAssignableFrom(targetEventType))
            {
                return true;
            }

            return false;
        }
    }
}