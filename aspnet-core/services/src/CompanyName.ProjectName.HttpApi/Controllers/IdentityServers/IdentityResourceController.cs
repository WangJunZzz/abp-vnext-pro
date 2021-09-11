using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.IdentityResources;
using CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos;
using CompanyName.ProjectName.Permissions;
using CompanyName.ProjectName.Publics.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/IdentityResource")]
    [Authorize(Policy = ProjectNamePermissions.IdentityServer.IdentityResources.Default)]
    public class IdentityResourceController : ProjectNameController
    {
        private readonly IIdentityResourceAppService _identityResourceAppService;

        public IdentityResourceController(IIdentityResourceAppService identityResourceAppService)
        {
            _identityResourceAppService = identityResourceAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取IdentityResource信息", Tags = new[] {"IdentityResource"})]
        public Task<PagedResultDto<PagingIdentityResourceListOutput>> GetListAsync(
            PagingIdentityResourceListInput input)
        {
            return _identityResourceAppService.GetListAsync(input);
        }
        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有IdentityResource信息", Tags = new[] {"IdentityResource"})]
        public Task<List<PagingIdentityResourceListOutput>> GetAllAsync()
        {
            return _identityResourceAppService.GetAllAsync();
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建IdentityResource", Tags = new[] {"IdentityResource"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.IdentityResources.Create)]
        public Task CreateAsync(CreateIdentityResourceInput input)
        {
            return _identityResourceAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新IdentityResource", Tags = new[] {"IdentityResource"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.IdentityResources.Update)]
        public Task UpdateAsync(UpdateIdentityResourceInput input)
        {
            return _identityResourceAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除IdentityResource", Tags = new[] {"IdentityResource"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.IdentityResources.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _identityResourceAppService.DeleteAsync(input);
        }
    }
}