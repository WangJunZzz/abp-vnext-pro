namespace Lion.AbpPro.Jobs
{
    public class TestJob : IRecurringJob
    {
        public Task ExecuteAsync()
        {
            Console.WriteLine($"job 测试- {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}