using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServer;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.ApiScopes;

namespace CompanyName.ProjectName.IdentityServers.ApiScopes
{
    public class ApiScopeAppService : ProjectNameAppService, IApiScopeAppService
    {
        private readonly IdenityServerApiScopeManager _idenityServerApiScopeManager;
        private readonly IdentityResourceManager _identityResourceManager;
        public ApiScopeAppService(IdenityServerApiScopeManager idenityServerApiScopeManager, IdentityResourceManager identityResourceManager)
        {
            _idenityServerApiScopeManager = idenityServerApiScopeManager;
            _identityResourceManager = identityResourceManager;
        }

        public async Task<PagedResultDto<PagingApiScopeListOutput>> GetListAsync(PagingApiScopeListInput input)
        {
            var list = await _idenityServerApiScopeManager.GetListAsync(
                input.SkipCount,
                input.PageSize,
                input.Filter,
                false);
            var totalCount = await _idenityServerApiScopeManager.GetCountAsync(input.Filter);
            return new PagedResultDto<PagingApiScopeListOutput>(totalCount,
                ObjectMapper.Map<List<ApiScope>, List<PagingApiScopeListOutput>>(list));
        }

        public Task CreateAsync(CreateApiScopeInput input)
        {
            return _idenityServerApiScopeManager.CreateAsync(input.Name, input.DisplayName, input.Description,
                input.Enabled, input.Required, input.Emphasize, input.ShowInDiscoveryDocument);
        }

        public Task UpdateAsync(UpdateCreateApiScopeInput input)
        {
            return _idenityServerApiScopeManager.UpdateAsync(input.Name, input.DisplayName, input.Description,
                input.Enabled, input.Required, input.Emphasize, input.ShowInDiscoveryDocument);
        }

        public Task DeleteAsync(IdInput input)
        {
            return _idenityServerApiScopeManager.DeleteAsync(input.Id);
        }

        public async Task<List<FromSelector<string, string>>> FindAllAsync()
        {
            var result=new  List<FromSelector<string, string>>();
            var apiScopes = await _idenityServerApiScopeManager.FindAllAsync();
            result.AddRange(apiScopes.Select(e => new FromSelector<string, string>(e.Name, e.DisplayName)).ToList());
            var identityResoure = await _identityResourceManager.GetAllAsync();
            result.AddRange(identityResoure.Select(e => new FromSelector<string, string>(e.Name, e.DisplayName)).ToList());
            return result;
        }
    }
}