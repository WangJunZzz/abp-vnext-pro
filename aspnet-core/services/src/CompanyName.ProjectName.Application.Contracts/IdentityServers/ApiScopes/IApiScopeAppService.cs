using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using Lion.Abp.Extension;
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