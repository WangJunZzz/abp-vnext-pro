using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos;
using CompanyName.ProjectName.Publics.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.IdentityServers.IdentityResources
{
    public interface IIdentityResourceAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingIdentityResourceListOutput>> GetListAsync(
            PagingIdentityResourceListInput input);

        /// <summary>
        /// 创建IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateIdentityResourceInput input);

        /// <summary>
        /// 更新IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateIdentityResourceInput input);

        /// <summary>
        /// 删除IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(IdInput input);
    }
}