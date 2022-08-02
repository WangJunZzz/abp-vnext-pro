namespace Lion.AbpPro.IdentityServers.IdentityResources
{
    [Authorize]
    public class IdentityResourceAppService : AbpProAppService, IIdentityResourceAppService
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
        [Authorize(Policy = AbpProPermissions.IdentityServer.IdentityResources.Create)]
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
        [Authorize(Policy = AbpProPermissions.IdentityServer.IdentityResources.Update)]
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
        [Authorize(Policy = AbpProPermissions.IdentityServer.IdentityResources.Delete)]
        public Task DeleteAsync(IdInput input)
        {
            return _identityResourceManager.DeleteAsync(input.Id);
        }
    }
}