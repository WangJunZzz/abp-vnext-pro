namespace Lion.AbpPro.WebGateway;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host .UseAutofac();
        await builder.AddApplicationAsync<AbpProWebGatewayModule>();
        var app = builder.Build();
        await app.InitializeApplicationAsync();
        await app.RunAsync();
    }
}