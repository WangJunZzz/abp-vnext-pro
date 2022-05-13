using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lion.AbpPro.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using Volo.Abp;

namespace Lion.AbpPro.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfigurationRoot _configurationRoot;
        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
            IConfigurationRoot configurationRoot)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configurationRoot = configurationRoot;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = await AbpApplicationFactory.CreateAsync<AbpProDbMigratorModule>(options =>
                   {
                       options.UseAutofac();
                       options.Services.AddLogging(c => c.AddSerilog());
                   }))
            {
                
                var s = _configurationRoot.GetValue<string>("ConnectionStrings:Default");
                await application.InitializeAsync();

                await application
                    .ServiceProvider
                    .GetRequiredService<AbpProDbMigrationService>()
                    .MigrateAsync();

                await application.ShutdownAsync();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
