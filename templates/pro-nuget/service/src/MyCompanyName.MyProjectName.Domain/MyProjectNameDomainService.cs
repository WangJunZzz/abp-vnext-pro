using Volo.Abp.EventBus.Local;

namespace MyCompanyName.MyProjectName
{
    public abstract class MyProjectNameDomainService : DomainService
    {
        protected Type ObjectMapperContext { get; set; }

        /// <summary>
        /// 工作单元管理器
        /// </summary>
        protected IUnitOfWorkManager UnitOfWorkManager =>
            LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

        /// <summary>
        /// 领域事件总线
        /// </summary>
        protected ILocalEventBus LocalEventBus =>
            LazyServiceProvider.LazyGetRequiredService<ILocalEventBus>();
        /// <summary>
        /// 分布式事件总线
        /// </summary>
        protected IDistributedEventBus DistributedEventBus =>
            LazyServiceProvider.LazyGetRequiredService<IDistributedEventBus>();
    }
}