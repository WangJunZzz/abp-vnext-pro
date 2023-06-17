using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace Lion.AbpPro.Cli;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp.IdentityModel", LogEventLevel.Information)
            .MinimumLevel.Override("Volo.Abp.Cli", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(CliPaths.Log, "lion.abp-pro-cli-logs.txt"))
            .WriteTo.Console()
            .CreateLogger();
        using var application = await AbpApplicationFactory.CreateAsync<AbpProCliModule>(
            options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });
        await application.InitializeAsync();

        await application.ServiceProvider
            .GetRequiredService<CliService>()
            .RunAsync(args);

        await application.ShutdownAsync();

        Log.CloseAndFlush();
    }
}