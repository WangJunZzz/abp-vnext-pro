using System.Collections.Generic;
using System.Threading.Tasks;
using Lion.AbpPro.IdentityServer;
using Lion.AbpPro.IdentityServers.Dtos;
using Lion.AbpPro.Extension.Customs.Dtos;
using Lion.AbpPro.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.ApiResources;

namespace Lion.AbpPro.IdentityServers.ApiResources
{
    [Authorize(Policy = AbpProPermissions.IdentityServer.ApiResource.Default)]
    public class ApiResourceAppService : AbpProAppService, IApiResourceAppService
    {
        private readonly IdenityServerApiResourceManager _idenityServerApiResourceManager;

        public ApiResourceAppService(IdenityServerApiResourceManager idenityServerApiResourceManager)
        {
            _idenityServerApiResourceManager = idenityServerApiResourceManager;
        }

        public async Task<PagedResultDto<ApiResourceOutput>> GetListAsync(PagingApiRseourceListInput input)
        {
            var list = await _idenityServerApiResourceManager.GetListAsync(
                input.SkipCount,
                input.PageSize,
                input.Filter,
                true);
            var totalCount = await _idenityServerApiResourceManager.GetCountAsync(input.Filter);
            return new PagedResultDto<ApiResourceOutput>(totalCount,
                ObjectMapper.Map<List<ApiResource>, List<ApiResourceOutput>>(list));
        }

        /// <summary>
        /// 获取所有api resource
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResourceOutput>> GetApiResources()
        {
            var list = await _idenityServerApiResourceManager.GetResources(false);
            return ObjectMapper.Map<List<ApiResource>, List<ApiResourceOutput>>(list);
        }

        /// <summary>
        /// 新增 ApiResource
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AbpProPermissions.IdentityServer.ApiResource.Create)]
        public Task CreateAsync(CreateApiResourceInput input)
        {
            return _idenityServerApiResourceManager.CreateAsync(
                GuidGenerator.Create(),
                input.Name,
                input.DisplayName,
                input.Description,
                input.Enabled,
                input.AllowedAccessTokenSigningAlgorithms,
                input.ShowInDiscoveryDocument,
                input.Secret
            );
        }

        /// <summary>
        /// 删除 ApiResource
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AbpProPermissions.IdentityServer.ApiResource.Delete)]
        public async Task DeleteAsync(IdInput input)
        {
            await _idenityServerApiResourceManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 更新 ApiResource
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AbpProPermissions.IdentityServer.ApiResource.Update)]
        public Task UpdateAsync(UpdateApiResourceInput input)
        {
            return _idenityServerApiResourceManager.UpdateAsync(
                input.Name,
                input.DisplayName,
                input.Description,
                input.Enabled,
                input.AllowedAccessTokenSigningAlgorithms,
                input.ShowInDiscoveryDocument,
                input.Secret,
                input.ApiScopes
            );
        }
    }
}