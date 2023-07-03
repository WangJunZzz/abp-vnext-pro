// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AbpProCapOptionsExtensions
    {
        public static CapOptions SetCapDbConnectionString(this CapOptions options, string dbConnectionString)
        {
            options.RegisterExtension(new AbpProEfCoreDbContextCapOptionsExtension
            {
                CapUsingDbConnectionString = dbConnectionString
            });
            
            return options;
        }
    }
}
