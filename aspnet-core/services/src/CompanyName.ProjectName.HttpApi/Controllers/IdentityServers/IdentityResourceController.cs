using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.IdentityResources;
using CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos;
using CompanyName.ProjectName.Publics.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/IdentityResource")]
    public class IdentityResourceController : ProjectNameController
    {
        private readonly IIdentityResourceAppService _identityResourceAppService;

        public IdentityResourceController(IIdentityResourceAppService identityResourceAppService)
        {
            _identityResourceAppService = identityResourceAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取IdentityResource信息", Tags = new[] {"IdentityServers"})]
        public Task<PagedResultDto<PagingIdentityResourceListOutput>> GetListAsync(
            PagingIdentityResourceListInput input)
        {
            return _identityResourceAppService.GetListAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建IdentityResource", Tags = new[] {"IdentityServers"})]
        public Task CreateAsync(CreateIdentityResourceInput input)
        {
            return _identityResourceAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新IdentityResource", Tags = new[] {"IdentityServers"})]
        public Task UpdateAsync(UpdateIdentityResourceInput input)
        {
            return _identityResourceAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除IdentityResource", Tags = new[] {"IdentityServers"})]
        public Task DeleteAsync(IdInput input)
        {
            return _identityResourceAppService.DeleteAsync(input);
        }
    }
}