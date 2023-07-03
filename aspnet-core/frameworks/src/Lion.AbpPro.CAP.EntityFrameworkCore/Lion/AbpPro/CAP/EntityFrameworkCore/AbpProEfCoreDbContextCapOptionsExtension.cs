namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public class AbpProEfCoreDbContextCapOptionsExtension : ICapOptionsExtension
{
    public string CapUsingDbConnectionString { get; init; }
    
    public void AddServices(IServiceCollection services)
    {
        services.Configure<AbpProEfCoreDbContextCapOptions>(options =>
        {
            options.CapUsingDbConnectionString = CapUsingDbConnectionString;
        });
    }
}