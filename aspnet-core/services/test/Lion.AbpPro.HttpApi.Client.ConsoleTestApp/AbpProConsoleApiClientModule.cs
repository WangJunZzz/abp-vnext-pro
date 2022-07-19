namespace Lion.AbpPro.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(AbpProHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class AbpProConsoleApiClientModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(
                        policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                    );
                });
            });
        }
    }
}
