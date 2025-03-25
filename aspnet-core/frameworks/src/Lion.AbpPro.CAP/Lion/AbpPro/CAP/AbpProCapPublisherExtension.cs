namespace Lion.AbpPro.CAP;

public static class AbpProCapPublisherExtension
{
    public static IDisposable UseTransaction(this ICapPublisher capPublisher, ICapTransaction capTransaction)
    {
        var previousValue = capPublisher.Transaction;
        capPublisher.Transaction = capTransaction;
        return new DisposeAction(() => capPublisher.Transaction = previousValue);
    }
}