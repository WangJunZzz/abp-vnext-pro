using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers;
using CompanyName.ProjectName.IdentityServers.Dtos;
using CompanyName.ProjectName.Publics.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/ApiResource")]
    public class ApiResourceController : ProjectNameController
    {
        private readonly IApiResourceAppService _apiResourceAppService;

        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            _apiResourceAppService = apiResourceAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取ApiResource信息", Tags = new[] {"IdentityServers"})]
        public Task<PagedResultDto<ApiResourceOutput>> GetListAsync(PagingApiRseourceListInput input)
        {
            return _apiResourceAppService.GetListAsync(input);
        }


        [HttpPost("all")]
        [SwaggerOperation(summary: "获取ApiResource信息", Tags = new[] {"IdentityServers"})]
        public Task<List<ApiResourceOutput>> GetApiResources()
        {
            return _apiResourceAppService.GetApiResources();
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "新增ApiResource", Tags = new[] {"IdentityServers"})]
        public Task CreateAsync(CreateApiResourceInput input)
        {
            return _apiResourceAppService.CreateAsync(input);
        }


        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除ApiResource", Tags = new[] {"IdentityServers"})]
        public async Task DeleteAsync(IdInput input)
        {
            await _apiResourceAppService.DeleteAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "删除ApiResource", Tags = new[] {"IdentityServers"})]
        public Task UpdateAsync(UpdateApiResourceInput input)
        {
            return _apiResourceAppService.UpdateAsync(input);
        }
    }
}