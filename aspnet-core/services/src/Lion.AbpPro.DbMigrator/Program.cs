using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Lion.AbpPro.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Lion.AbpPro", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Lion.AbpPro", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureAppConfiguration(otpions =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    Console.WriteLine($"ASPNETCORE_ENVIRONMENT:{environment}");
                    var appSettingFileName =  "appsettings.json";
                    if (!environment.IsNullOrWhiteSpace())
                        appSettingFileName = $"appsettings.{environment}.json";
                    Console.WriteLine($"appSettingFileName:{appSettingFileName}");
                    otpions.AddJsonFile(appSettingFileName,optional:false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                   var s = hostContext.HostingEnvironment;
                  var _configurationRoot= services.GetRequiredService<IConfigurationRoot>();
                  var ss = _configurationRoot.GetValue<string>("ConnectionStrings:Default");
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }
}
