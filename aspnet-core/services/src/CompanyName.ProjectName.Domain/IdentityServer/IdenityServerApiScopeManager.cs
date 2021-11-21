using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.ApiScopes;

namespace CompanyName.ProjectName.IdentityServer
{
    public class IdenityServerApiScopeManager : ProjectNameDomainService
    {
        private readonly IApiScopeRepository _apiScopeRepository;

        public IdenityServerApiScopeManager(IApiScopeRepository apiScopeRepository)
        {
            _apiScopeRepository = apiScopeRepository;
        }

        public Task<List<ApiScope>> GetListAsync(
            int skipCount = 0,
            int maxResultCount = 10,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return _apiScopeRepository.GetListAsync(
                "CreationTime desc",
                skipCount,
                maxResultCount,
                filter,
                includeDetails,
                cancellationToken);
        }

        public Task<long> GetCountAsync(string filter = null,
            CancellationToken cancellationToken = default)
        {
            return _apiScopeRepository.GetCountAsync(filter, cancellationToken);
        }

        public async Task<ApiScope> CreateAsync(
            string name,
            string displayName,
            string description,
            bool enabled,
            bool required,
            bool emphasize,
            bool showInDiscoveryDocument)
        {
            var apiScope = await _apiScopeRepository.GetByNameAsync(name, false);
            if (null != apiScope) throw new UserFriendlyException(message: $"{name}已存在");

            apiScope = new ApiScope(GuidGenerator.Create(), name, displayName, description,
                required, emphasize,
                showInDiscoveryDocument, enabled);
            return await _apiScopeRepository.InsertAsync(apiScope);
        }

        public async Task<ApiScope> UpdateAsync(
            string name,
            string displayName,
            string description,
            bool enabled,
            bool required,
            bool emphasize,
            bool showInDiscoveryDocument)
        {
            var apiScope = await _apiScopeRepository.GetByNameAsync(name, false);
            if (null == apiScope) throw new UserFriendlyException(message: $"{name}不存在");
            apiScope.DisplayName = displayName;
            apiScope.Description = description;
            apiScope.Enabled = enabled;
            apiScope.Required = required;
            apiScope.Emphasize = emphasize;
            apiScope.ShowInDiscoveryDocument = showInDiscoveryDocument;
            return await _apiScopeRepository.UpdateAsync(apiScope);
        }

        public Task DeleteAsync(Guid id, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            return _apiScopeRepository.DeleteAsync(id, autoSave, cancellationToken);
        }

        public async Task<List<ApiScope>> FindAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _apiScopeRepository.GetListAsync(e => e.Enabled == true, false,
                cancellationToken);
        }
    }
}