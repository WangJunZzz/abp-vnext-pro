using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using CompanyName.ProjectName.Publics.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.IdentityServers.ApiScopes
{
    public interface IApiScopeAppService : IApplicationService
    {
        Task<PagedResultDto<PagingApiScopeListOutput>> GetListAsync(PagingApiScopeListInput input);

        Task CreateAsync(CreateApiScopeInput input);

        Task UpdateAsync(UpdateCreateApiScopeInput input);
        
        Task DeleteAsync(IdInput input);

        Task<List<FromSelector<string, string>>> FindAllAsync();
    }
}