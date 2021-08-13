// using Microsoft.Extensions.DependencyInjection;
// using Volo.Abp.Http.Client;
// using Volo.Abp.Modularity;
//
// namespace CompanyName.ProjectName.DataDictionaryManagement
// {
//     [DependsOn(
//         typeof(DataDictionaryManagementApplicationContractsModule),
//         typeof(AbpHttpClientModule))]
//     public class DataDictionaryManagementHttpApiClientModule : AbpModule
//     {
//         public const string RemoteServiceName = "DataDictionaryManagement";
//
//         public override void ConfigureServices(ServiceConfigurationContext context)
//         {
//             context.Services.AddHttpClientProxies(
//                 typeof(DataDictionaryManagementApplicationContractsModule).Assembly,
//                 RemoteServiceName
//             );
//         }
//     }
// }
