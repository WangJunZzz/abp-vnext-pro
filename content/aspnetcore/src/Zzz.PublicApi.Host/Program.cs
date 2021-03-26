using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zzz.Extensions;

namespace Zzz.PublicApi.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, builder) => {
                    // ÅäÖÃnacos
                    builder.AddNacosConfiguration(builder.Build().GetSection("NacosConfig"));
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    SerilogToEsExtensions.SetSerilogConfiguration(
                        loggerConfiguration,
                        context.Configuration);
                }).UseAutofac();
    }
}
