using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServers.Dtos;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.ApiResources;
using IdentityModel;

namespace CompanyName.ProjectName.IdentityServer
{
    public class IdenityServerApiResourceManager : DomainService
    {
        private readonly IApiResourceRepository _apiResourceRepository;

        public IdenityServerApiResourceManager(IApiResourceRepository apiResourceRepository)
        {
            _apiResourceRepository = apiResourceRepository;
        }

        public Task<List<ApiResource>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return _apiResourceRepository.GetListAsync("CreationTime", skipCount, maxResultCount, filter,
                includeDetails,
                cancellationToken);
        }

        public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return _apiResourceRepository.GetCountAsync(filter,
                cancellationToken);
        }

        /// <summary>
        /// 获取所有api resource
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResource>> GetResources(
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await _apiResourceRepository.GetListAsync(includeDetails, cancellationToken);
        }

        public async Task<ApiResource> CreateAsync(
            Guid id,
            string name,
            string displayName,
            string description,
            bool enabled,
            string allowedAccessTokenSigningAlgorithms,
            bool showInDiscoveryDocument,
            List<ApiResourceSecretOutput> secrets = null,
            List<ApiResourceScopeOutput> scopes = null,
            List<ApiResourceClaimOutput> claims = null,
            List<ApiResourcePropertyOutput> properties = null,
            CancellationToken cancellationToken = default)
        {
            var apiResource =
                await _apiResourceRepository.FindByNameAsync(name.Trim(), false, cancellationToken);
            if (null != apiResource) throw new UserFriendlyException(message: "ApiResource已存在");
            apiResource = new ApiResource(id, name, displayName, description)
            {
                AllowedAccessTokenSigningAlgorithms = allowedAccessTokenSigningAlgorithms,
                ShowInDiscoveryDocument = showInDiscoveryDocument,
                Enabled = enabled
            };

            secrets?.Distinct().ToList().ForEach(item =>
            {
                apiResource.AddSecret(item.Value.ToSha256(), item.Expiration, item.Type, item.Description);
            });

            scopes?.Distinct().ToList().ForEach(item => { apiResource.AddScope(item.Scope); });

            claims?.Distinct().ToList().ForEach(item => { apiResource.AddUserClaim(item.Type); });

            properties?.Distinct().ToList().ForEach(item => { apiResource.AddProperty(item.Key, item.Value); });

            return await _apiResourceRepository.InsertAsync(apiResource, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            Guid id,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            await _apiResourceRepository.DeleteAsync(id, autoSave, cancellationToken);
        }

        public async Task<ApiResource> UpdateAsync(
            string name,
            string displayName,
            string description,
            bool enabled,
            string allowedAccessTokenSigningAlgorithms,
            bool showInDiscoveryDocument,
            List<ApiResourceSecretOutput> secrets = null,
            List<ApiResourceScopeOutput> scopes = null,
            List<ApiResourceClaimOutput> claims = null,
            List<ApiResourcePropertyOutput> properties = null,
            CancellationToken cancellationToken = default
        )
        {
            var apiResource =
                await _apiResourceRepository.FindByNameAsync(name.Trim(), false, cancellationToken);
            if (null == apiResource) throw new UserFriendlyException(message: "ApiResource不存在");
            apiResource.DisplayName = displayName;
            apiResource.Description = description;
            apiResource.Enabled = enabled;
            apiResource.AllowedAccessTokenSigningAlgorithms = allowedAccessTokenSigningAlgorithms;
            apiResource.ShowInDiscoveryDocument = showInDiscoveryDocument;
            secrets?.Distinct().ToList().ForEach(item =>
            {
                apiResource.AddSecret(item.Value.ToSha256(), item.Expiration, item.Type, item.Description);
            });

            scopes?.Distinct().ToList().ForEach(item => { apiResource.AddScope(item.Scope); });

            claims?.Distinct().ToList().ForEach(item => { apiResource.AddUserClaim(item.Type); });

            properties?.Distinct().ToList().ForEach(item => { apiResource.AddProperty(item.Key, item.Value); });
            return await _apiResourceRepository.UpdateAsync(apiResource, cancellationToken: cancellationToken);
        }
    }
}