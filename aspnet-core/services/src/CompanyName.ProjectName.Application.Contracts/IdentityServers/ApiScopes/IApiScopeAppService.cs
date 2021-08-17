using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using CompanyName.ProjectName.Publics.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.IdentityServers.ApiScopes
{
    public interface IApiScopeAppService : IApplicationService
    {
        Task<PagedResultDto<ApiScopeOutput>> GetListAsync(PagingApiScopeListInput input);

        Task CreateAsync(CreateApiScopeInput input);

        Task UpdateAsync(UpdateCreateApiScopeInput input);
        
        Task DeleteAsync(IdInput input);
    }
}