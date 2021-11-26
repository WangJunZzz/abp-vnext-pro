// using System;
// using System.Threading.Tasks;
// using Lion.AbpPro.Tenants.Dtos;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
// using Volo.Abp.Application.Services;
// using Volo.Abp.AspNetCore.Mvc.MultiTenancy;
//
// namespace Lion.AbpPro.Controllers.Tenants
// {
//     [Route("Tenants")]
//     public class AbpTenantController : AbpProController,IApplicationService
//     {
//         private readonly IAbpTenantAppService _abpTenantAppService;
//
//         public AbpTenantController(IAbpTenantAppService abpTenantAppService)
//         {
//             _abpTenantAppService = abpTenantAppService;
//         }
//         
//         [HttpPost("find")]
//         [SwaggerOperation(summary: "通过名称获取租户信息", Tags = new[] {"Tenants"})]
//         public async Task<FindTenantResultDto> FindTenantByNameAsync(FindTenantByNameInput input)
//         {
//             return await _abpTenantAppService.FindTenantByNameAsync(input.Name);
//         }
//     }
// }