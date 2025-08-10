namespace Lion.AbpPro.Hangfire;

public interface IRecurringJob : ITransientDependency
{
    /// <summary>
    /// 执行任务
    /// </summary>
    Task ExecuteAsync();
}