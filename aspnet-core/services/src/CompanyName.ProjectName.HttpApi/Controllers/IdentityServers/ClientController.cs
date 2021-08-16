using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.Clients;
using CompanyName.ProjectName.Publics.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/Client")]
    public class ClientController : ProjectNameController
    {
        private readonly IIdentityServerClientAppService _identityServerClientAppService;

        public ClientController(IIdentityServerClientAppService identityServerClientAppService)
        {
            _identityServerClientAppService = identityServerClientAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取Client信息", Tags = new[] {"IdentityServers"})]
        public Task<PagedResultDto<ClientOutput>> GetListAsync(PagingClientListInput input)
        {
            return _identityServerClientAppService.GetListAsync(input);
        }


        [HttpPost("create")]
        [SwaggerOperation(summary: "创建Client", Tags = new[] {"IdentityServers"})]
        public Task CreateAsync(CreateClientInput input)
        {
            return _identityServerClientAppService.CreateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除client", Tags = new[] {"IdentityServers"})]
        public Task DeleteAsync(IdInput input)
        {
            return _identityServerClientAppService.DeleteAsync(input);
        }

        [HttpPost("basic/update")]
        [SwaggerOperation(summary: "更新基本信息", Tags = new[] {"IdentityServers"})]
        public Task UpdateBasicDataAsync(UpdataBasicDataInput input)
        {
            return _identityServerClientAppService.UpdateBasicDataAsync(input);
        }

        [HttpPost("scopes/update")]
        [SwaggerOperation(summary: "更新client scopes", Tags = new[] {"IdentityServers"})]
        public Task UpdateScopesAsync(UpdateScopeInput input)
        {
            return _identityServerClientAppService.UpdateScopesAsync(input);
        }

        [HttpPost("redirect/uri/add")]
        [SwaggerOperation(summary: "新增回调地址", Tags = new[] {"IdentityServers"})]
        public Task AddRedirectUriAsync(AddRedirectUriInput input)
        {
            return _identityServerClientAppService.AddRedirectUriAsync(input);
        }

        [HttpPost("redirect/uri/remove")]
        [SwaggerOperation(summary: "删除回调地址", Tags = new[] {"IdentityServers"})]
        public Task RemoveRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _identityServerClientAppService.RemoveRedirectUriAsync(input);
        }

        [HttpPost("logout/redirect/uri/add")]
        [SwaggerOperation(summary: "新增Logout回调地址", Tags = new[] {"IdentityServers"})]
        public Task AddLogoutRedirectUriAsync(AddRedirectUriInput input)
        {
            return _identityServerClientAppService.AddLogoutRedirectUriAsync(input);
        }

        [HttpPost("logout/redirect/uri/remove")]
        [SwaggerOperation(summary: "删除Logout回调地址", Tags = new[] {"IdentityServers"})]
        public Task RemoveLogoutRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _identityServerClientAppService.RemoveLogoutRedirectUriAsync(input);
        }

        [HttpPost("cors/add")]
        [SwaggerOperation(summary: "添加cors", Tags = new[] {"IdentityServers"})]
        public Task AddCorsAsync(AddCorsInput input)
        {
            return _identityServerClientAppService.AddCorsAsync(input);
        }

        [HttpPost("cors/remove")]
        [SwaggerOperation(summary: "删除cors", Tags = new[] {"IdentityServers"})]
        public Task RemoveCorsAsync(RemoveCorsInput input)
        {
            return _identityServerClientAppService.RemoveCorsAsync(input);
        }
    }
}