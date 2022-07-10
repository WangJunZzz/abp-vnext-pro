namespace Lion.AbpPro.Extensions.Hangfire;

public class AutoDeleteAfterSuccessAttributer : JobFilterAttribute, IApplyStateFilter
{
    private readonly TimeSpan _deleteAfter;

    public AutoDeleteAfterSuccessAttributer(TimeSpan timeSpan)
    {
        _deleteAfter = timeSpan;
    }

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        context.JobExpirationTimeout = _deleteAfter;
    }

    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
     
    }
}