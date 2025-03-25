namespace Lion.AbpPro.CAP;

public class AbpProCapDistributedEventBus : EventBusBase, IDistributedEventBus, ISingletonDependency
{
    protected AbpDistributedEventBusOptions AbpDistributedEventBusOptions { get; }
    protected readonly ICapPublisher CapPublisher;

    //TODO: Accessing to the List<IEventHandlerFactory> may not be thread-safe!
    protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }
    protected ConcurrentDictionary<string, Type> EventTypes { get; }

    public AbpProCapDistributedEventBus(IServiceScopeFactory serviceScopeFactory,
        IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions,
        ICapPublisher capPublisher,
        IUnitOfWorkManager unitOfWorkManager,
        ICurrentTenant currentTenant,
        IEventHandlerInvoker eventHandlerInvoker)
        : base(serviceScopeFactory, currentTenant, unitOfWorkManager, eventHandlerInvoker)
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

    public IDisposable Subscribe<TEvent>(IDistributedEventHandler<TEvent> handler) where TEvent : class
    {
        return Subscribe(typeof(TEvent), handler);
    }

    public virtual Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true,
        bool useOutbox = true) where TEvent : class
    {
        return PublishAsync(typeof(TEvent), eventData, onUnitOfWorkComplete, useOutbox);
    }

    public virtual async Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true,
        bool useOutbox = true)
    {
        if (onUnitOfWorkComplete && UnitOfWorkManager.Current != null)
        {
            AddToUnitOfWork(
                UnitOfWorkManager.Current,
                new UnitOfWorkEventRecord(eventType, eventData, EventOrderGenerator.GetNext(), useOutbox)
            );
            return;
        }

        if (useOutbox && UnitOfWorkManager.Current != null)
        {
            if (UnitOfWorkManager.Current is not AbpProCapUnitOfWork capUnitOfWork || capUnitOfWork.CapTransaction is null)
            {
                UnitOfWorkManager.Current.OnCompleted(async () =>
                {
                    await PublishToEventBusAsync(eventType, eventData);
                });
            }
            else
            {
                using (CapPublisher.UseTransaction(capUnitOfWork.CapTransaction))
                {
                    // Use CAP transactional outbox
                    await PublishToEventBusAsync(eventType, eventData);
                }
            }

            return;
        }

        await PublishToEventBusAsync(eventType, eventData);
    }

    protected override async Task PublishToEventBusAsync(Type eventType, object eventData)
    {
        var eventName = EventNameAttribute.GetNameOrDefault(eventType);
        var header = new Dictionary<string, string>
        {
            { AbpProCapConsts.Tenant, CurrentTenant.Id?.ToString() ?? string.Empty },
        };
        await CapPublisher.PublishAsync(eventName, eventData, header);
    }

    protected override void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord)
    {
        unitOfWork.AddOrReplaceDistributedEvent(eventRecord);
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