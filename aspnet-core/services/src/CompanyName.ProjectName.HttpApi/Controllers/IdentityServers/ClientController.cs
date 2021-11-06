using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.Clients;
using CompanyName.ProjectName.Permissions;
using Lion.Abp.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/Client")]
    [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Default)]
    public class ClientController : ProjectNameController,IIdentityServerClientAppService
    {
        private readonly IIdentityServerClientAppService _identityServerClientAppService;

        public ClientController(IIdentityServerClientAppService identityServerClientAppService)
        {
            _identityServerClientAppService = identityServerClientAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取Client信息", Tags = new[] {"Client"})]
        public Task<PagedResultDto<PagingClientListOutput>> GetListAsync(PagingClientListInput input)
        {
            return _identityServerClientAppService.GetListAsync(input);
        }


        [HttpPost("create")]
        [SwaggerOperation(summary: "创建Client", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Create)]
        public Task CreateAsync(CreateClientInput input)
        {
            return _identityServerClientAppService.CreateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除client", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _identityServerClientAppService.DeleteAsync(input);
        }

        [HttpPost("updateBasic")]
        [SwaggerOperation(summary: "更新基本信息", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task UpdateBasicDataAsync(UpdataBasicDataInput input)
        {
            return _identityServerClientAppService.UpdateBasicDataAsync(input);
        }

        [HttpPost("updateScopes")]
        [SwaggerOperation(summary: "更新client scopes", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task UpdateScopesAsync(UpdateScopeInput input)
        {
            return _identityServerClientAppService.UpdateScopesAsync(input);
        }

        [HttpPost("addRedirectUri")]
        [SwaggerOperation(summary: "新增回调地址", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task AddRedirectUriAsync(AddRedirectUriInput input)
        {
            return _identityServerClientAppService.AddRedirectUriAsync(input);
        }

        [HttpPost("removeRedirectUri")]
        [SwaggerOperation(summary: "删除回调地址", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task RemoveRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _identityServerClientAppService.RemoveRedirectUriAsync(input);
        }

        [HttpPost("addLogoutRedirectUri")]
        [SwaggerOperation(summary: "新增Logout回调地址", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task AddLogoutRedirectUriAsync(AddRedirectUriInput input)
        {
            return _identityServerClientAppService.AddLogoutRedirectUriAsync(input);
        }

        [HttpPost("removeLogoutRedirectUri")]
        [SwaggerOperation(summary: "删除Logout回调地址", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task RemoveLogoutRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _identityServerClientAppService.RemoveLogoutRedirectUriAsync(input);
        }

        [HttpPost("addCors")]
        [SwaggerOperation(summary: "添加cors", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task AddCorsAsync(AddCorsInput input)
        {
            return _identityServerClientAppService.AddCorsAsync(input);
        }

        [HttpPost("removeCors")]
        [SwaggerOperation(summary: "删除cors", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Update)]
        public Task RemoveCorsAsync(RemoveCorsInput input)
        {
            return _identityServerClientAppService.RemoveCorsAsync(input);
        }

        [HttpPost("enabled")]
        [SwaggerOperation(summary: "禁用client", Tags = new[] {"Client"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.Client.Enable)]
        public Task EnabledAsync(EnabledInput input)
        {
            return _identityServerClientAppService.EnabledAsync(input);
        }
    }
}