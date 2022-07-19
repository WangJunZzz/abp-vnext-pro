namespace Lion.AbpPro.FreeSqlRepository;

public abstract class FreeSqlBasicRepository : DomainService
{
    protected IFreeSql FreeSql => LazyServiceProvider.LazyGetRequiredService<IFreeSql>();

    private ICancellationTokenProvider CancellationTokenProvider =>
        LazyServiceProvider.LazyGetService<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);

    protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
    {
        return CancellationTokenProvider.FallbackToProvider(preferredValue);
    }
}