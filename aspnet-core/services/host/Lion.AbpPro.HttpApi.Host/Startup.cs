namespace Lion.AbpPro
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<AbpProHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app,IHostApplicationLifetime lifetime)
        {
            app.InitializeApplication();
        }
    }
}
