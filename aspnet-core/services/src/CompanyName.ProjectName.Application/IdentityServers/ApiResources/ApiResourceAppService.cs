using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServer;
using CompanyName.ProjectName.IdentityServers.Dtos;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.ApiResources;

namespace CompanyName.ProjectName.IdentityServers.ApiResources
{
    public class ApiResourceAppService : ProjectNameAppService, IApiResourceAppService
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
        public async Task DeleteAsync(IdInput input)
        {
            await _idenityServerApiResourceManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 更新 ApiResource
        /// </summary>
        /// <returns></returns>
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