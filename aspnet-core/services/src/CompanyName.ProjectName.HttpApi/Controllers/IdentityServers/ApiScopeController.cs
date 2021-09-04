using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.IdentityServers.ApiScopes;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using CompanyName.ProjectName.Permissions;
using CompanyName.ProjectName.Publics.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.Controllers.IdentityServers
{
    [Route("IdentityServer/ApiScope")]
    [Authorize(Policy = ProjectNamePermissions.IdentityServer.ApiScope.Default)]
    public class ApiScopeController:ProjectNameController
    {
        private readonly IApiScopeAppService _apiScopeAppService;

        public ApiScopeController(IApiScopeAppService apiScopeAppService)
        {
            _apiScopeAppService = apiScopeAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取ApiScope信息", Tags = new[] {"ApiScope"})]
        public Task<PagedResultDto<PagingApiScopeListOutput>> GetListAsync(PagingApiScopeListInput input)
        {
            return _apiScopeAppService.GetListAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建ApiScope", Tags = new[] {"ApiScope"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.ApiScope.Create)]
        public Task CreateAsync(CreateApiScopeInput input)
        {
            return _apiScopeAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "更新ApiScope", Tags = new[] {"ApiScope"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.ApiScope.Update)]
        public Task UpdateAsync(UpdateCreateApiScopeInput input)
        {
            return _apiScopeAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除ApiScope", Tags = new[] {"ApiScope"})]
        [Authorize(Policy = ProjectNamePermissions.IdentityServer.ApiScope.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _apiScopeAppService.DeleteAsync(input);
        }
        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有ApiScope", Tags = new[] {"ApiScope"})]
        public  Task<List<FromSelector<string, string>>> FindAllAsync()
        {
            return _apiScopeAppService.FindAllAsync();
        }
    }
}