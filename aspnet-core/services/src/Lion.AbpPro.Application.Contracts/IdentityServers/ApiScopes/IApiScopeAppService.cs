using System.Collections.Generic;
using System.Threading.Tasks;
using Lion.AbpPro.IdentityServers.ApiScopes.Dtos;
using Lion.AbpPro.Extension.Customs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.IdentityServers.ApiScopes
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