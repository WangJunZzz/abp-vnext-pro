using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServer;
using CompanyName.ProjectName.IdentityServers.IdentityResources;
using CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.IdentityResources;

namespace CompanyName.ProjectName.IdentityServers.Mappers.IdentityResources
{
    public class IdentityResourceAppService : ProjectNameAppService, IIdentityResourceAppService
    {
        private readonly IdentityResourceManager _identityResourceManager;

        public IdentityResourceAppService(IdentityResourceManager identityResourceManager)
        {
            _identityResourceManager = identityResourceManager;
        }

        /// <summary>
        /// 分页获取IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PagingIdentityResourceListOutput>> GetListAsync(
            PagingIdentityResourceListInput input)
        {
            var list = await _identityResourceManager.GetListAsync(
                input.SkipCount,
                input.PageSize,
                input.Filter,
                true);
            var totalCount = await _identityResourceManager.GetCountAsync(input.Filter);
            return new PagedResultDto<PagingIdentityResourceListOutput>(totalCount,
                ObjectMapper.Map<List<IdentityResource>, List<PagingIdentityResourceListOutput>>(list));
        }

        public async Task<List<PagingIdentityResourceListOutput>> GetAllAsync()
        {
            var list = await _identityResourceManager.GetAllAsync();
            return ObjectMapper.Map<List<IdentityResource>, List<PagingIdentityResourceListOutput>>(list);
        }

        /// <summary>
        /// 创建IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task CreateAsync(CreateIdentityResourceInput input)
        {
            return _identityResourceManager.CreateAsync(input.Name, input.DisplayName, input.Description,
                input.Enabled, input.Required, input.Emphasize, input.ShowInDiscoveryDocument);
        }

        /// <summary>
        /// 更新IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task UpdateAsync(UpdateIdentityResourceInput input)
        {
            return _identityResourceManager.UpdateAsync(input.Name, input.DisplayName, input.Description,
                input.Enabled, input.Required, input.Emphasize, input.ShowInDiscoveryDocument);
        }

        /// <summary>
        /// 删除IdentityResource
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task DeleteAsync(IdInput input)
        {
            return _identityResourceManager.DeleteAsync(input.Id);
        }
    }
}