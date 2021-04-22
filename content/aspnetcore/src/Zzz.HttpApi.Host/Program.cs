using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Zzz.Extensions;

namespace Zzz
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Starting Zzz.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    SerilogToEsExtensions.SetSerilogConfiguration(
                        loggerConfiguration,
                        context.Configuration);
                })
                .UseAutofac();
         
    }
}
