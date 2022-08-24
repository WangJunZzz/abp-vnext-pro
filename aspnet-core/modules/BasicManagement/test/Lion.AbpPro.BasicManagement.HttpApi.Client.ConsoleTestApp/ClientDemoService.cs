using Microsoft.Extensions.Configuration;
using Volo.Abp.DependencyInjection;
using Volo.Abp.IdentityModel;

namespace Lion.AbpPro.BasicManagement;

public class ClientDemoService : ITransientDependency
{
    private readonly IIdentityModelAuthenticationService _authenticationService;
    private readonly IConfiguration _configuration;

    public ClientDemoService(
        IIdentityModelAuthenticationService authenticationService,
        IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
    }

    public async Task RunAsync()
    {
        await Task.CompletedTask;
    }
}