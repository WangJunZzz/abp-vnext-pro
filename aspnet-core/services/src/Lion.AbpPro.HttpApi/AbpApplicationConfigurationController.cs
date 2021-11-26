// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Volo.Abp.Application.Services;
// using Volo.Abp.AspNetCore.Mvc;
// using Volo.Abp.AspNetCore.Mvc.AntiForgery;
// using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
// using Volo.Abp.DependencyInjection;
//
// namespace Lion.AbpPro.WebGateway.Controllers
// {
//     [Dependency(ReplaceServices = true)]
//     [ExposeServices(typeof(IAbpApplicationConfigurationAppService))]
//     [Route("AbpPro")]
//     public class AbpProAbpApplicationConfigurationController : AbpController,IAbpApplicationConfigurationAppService
//     {
//         private readonly IAbpApplicationConfigurationAppService _applicationConfigurationAppService;
//         private readonly IAbpAntiForgeryManager _antiForgeryManager;
//
//         public AbpProAbpApplicationConfigurationController(
//             IAbpApplicationConfigurationAppService applicationConfigurationAppService,
//             IAbpAntiForgeryManager antiForgeryManager)
//         {
//             _applicationConfigurationAppService = applicationConfigurationAppService;
//             _antiForgeryManager = antiForgeryManager;
//         }
//
//         [HttpGet("api/abp/application-configuration")]
//         public async Task<ApplicationConfigurationDto> GetAsync()
//         {
//             _antiForgeryManager.SetCookie();
//             return await _applicationConfigurationAppService.GetAsync();
//         }
//     }
// }