namespace Lion.AbpPro.Extensions.Hangfire;

public class AutoDeleteAfterSuccessAttribute : JobFilterAttribute, IApplyStateFilter
{
    private readonly TimeSpan _deleteAfter;

    public AutoDeleteAfterSuccessAttribute(TimeSpan timeSpan)
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