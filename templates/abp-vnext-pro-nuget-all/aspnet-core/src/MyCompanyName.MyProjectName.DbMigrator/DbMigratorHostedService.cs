namespace MyCompanyName.MyProjectName.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
            IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = await AbpApplicationFactory.CreateAsync<MyProjectNameDbMigratorModule>(options =>
                   {
                       options.Services.ReplaceConfiguration(_configuration);
                       options.UseAutofac();
                       options.Services.AddLogging(c => c.AddSerilog());
                   }))
            {
                await application.InitializeAsync();
                var conn = _configuration.GetValue<string>("ConnectionStrings:Default");
                Console.WriteLine("ConnectionStrings:" + conn);
                var s = _hostEnvironment.EnvironmentName;
                Console.WriteLine("EnvironmentName:" + s);
                await application
                    .ServiceProvider
                    .GetRequiredService<MyProjectNameDbMigrationService>()
                    .MigrateAsync();
                
                await application.ShutdownAsync();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
