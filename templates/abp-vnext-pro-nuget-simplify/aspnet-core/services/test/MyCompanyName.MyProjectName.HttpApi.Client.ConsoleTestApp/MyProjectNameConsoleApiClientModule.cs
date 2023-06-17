namespace MyCompanyName.MyProjectName.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(MyProjectNameHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MyProjectNameConsoleApiClientModule : AbpModule
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
