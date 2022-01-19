using System.Threading.Tasks;
using Lion.AbpPro.Extension.Customs.Dtos;
using Lion.AbpPro.IdentityServers.Clients.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.IdentityServers.Clients
{
    public interface IIdentityServerClientAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询Client
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<PagingClientListOutput>> GetListAsync(PagingClientListInput input);

        /// <summary>
        /// 创建Client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateClientInput input);

        /// <summary>
        /// 删除client
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(IdInput input);

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <returns></returns>
        Task UpdateBasicDataAsync(UpdataBasicDataInput input);

        /// <summary>
        /// 更新client scopes
        /// </summary>
        /// <returns></returns>
        Task UpdateScopesAsync(UpdateScopeInput input);

        /// <summary>
        /// 新增回调地址
        /// </summary>
        Task AddRedirectUriAsync(AddRedirectUriInput input);

        /// <summary>
        /// 删除回调地址
        /// </summary>
        Task RemoveRedirectUriAsync(RemoveRedirectUriInput input);

        /// <summary>
        /// 新增Logout回调地址
        /// </summary>
        Task AddLogoutRedirectUriAsync(AddRedirectUriInput input);

        /// <summary>
        /// 删除Logout回调地址
        /// </summary>
        Task RemoveLogoutRedirectUriAsync(RemoveRedirectUriInput input);

        /// <summary>
        /// 添加cors
        /// </summary>
        Task AddCorsAsync(AddCorsInput input);

        /// <summary>
        /// 删除cors
        /// </summary>
        Task RemoveCorsAsync(RemoveCorsInput input);

        /// <summary>
        /// 禁用client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task EnabledAsync(EnabledInput input);
    }
}