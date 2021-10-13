using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.Clients;

namespace CompanyName.ProjectName.IdentityServer
{
    public class IdenityServerClientManager : DomainService
    {
        private readonly IClientRepository _clientRepository;

        public IdenityServerClientManager(
            IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public Task<List<Client>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return _clientRepository.GetListAsync("CreationTime desc", skipCount, maxResultCount, filter, includeDetails,
                cancellationToken);
        }

        public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return _clientRepository.GetCountAsync(filter,
                cancellationToken);
        }

        public Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return _clientRepository.DeleteAsync(id, autoSave, default);
        }

        public async Task<Client> CreateAsync(string clientId, string clientName, string description, string allowedGrantTypes)
        {
            var entity = await _clientRepository.FindByClientIdAsync(clientId);
            if (null != entity) throw new UserFriendlyException(message: "当前ClientId已存在");
            entity = new Client(GuidGenerator.Create(), clientId)
            {
                ClientName = clientName, Description = description
            };
            entity.ClientClaimsPrefix="client_";
            entity.AddGrantType(allowedGrantTypes);
            return await _clientRepository.InsertAsync(entity);
        }

        public async Task<Client> UpdateBasicDataAsync(
            string clientId,
            string clientName,
            string description,
            string clientUri,
            string logoUri,
            bool enabled,
            string protocolType,
            bool requireClientSecret,
            bool requireConsent,
            bool allowRememberConsent,
            bool alwaysIncludeUserClaimsInIdToken,
            bool requirePkce,
            bool allowPlainTextPkce,
            bool requireRequestObject,
            bool allowAccessTokensViaBrowser,
            string frontChannelLogoutUri,
            bool frontChannelLogoutSessionRequired,
            string backChannelLogoutUri,
            bool backChannelLogoutSessionRequired,
            bool allowOfflineAccess,
            int identityTokenLifetime,
            string allowedIdentityTokenSigningAlgorithms,
            int accessTokenLifetime,
            int authorizationCodeLifetime,
            int? consentLifetime,
            int absoluteRefreshTokenLifetime,
            int refreshTokenUsage,
            bool updateAccessTokenClaimsOnRefresh,
            int refreshTokenExpiration,
            int accessTokenType,
            bool enableLocalLogin,
            bool includeJwtId,
            bool alwaysSendClientClaims,
            string clientClaimsPrefix,
            string pairWiseSubjectSalt,
            int? userSsoLifetime,
            string userCodeType,
            int deviceCodeLifetime,
            int slidingRefreshTokenLifetime,
            string secret,
            string secretType,
            string allowGrantType
        )
        {
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null)
            {
                throw new UserFriendlyException(message: $"{clientId}不存在");
            }

            client.ClientName = clientName;
            client.Description = description;
            client.ClientUri = clientUri;
            client.LogoUri = logoUri;
            client.FrontChannelLogoutUri = frontChannelLogoutUri;
            client.BackChannelLogoutUri = backChannelLogoutUri;
            client.AllowedIdentityTokenSigningAlgorithms = allowedIdentityTokenSigningAlgorithms;
            client.Enabled = enabled;
            client.RequireConsent = requireConsent;
            client.RequireRequestObject = requireRequestObject;
            client.AllowRememberConsent = allowRememberConsent;
            client.AllowOfflineAccess = allowOfflineAccess;
            client.FrontChannelLogoutSessionRequired = frontChannelLogoutSessionRequired;
            client.BackChannelLogoutSessionRequired = backChannelLogoutSessionRequired;
            client.IncludeJwtId = includeJwtId;
            client.RequirePkce = requirePkce;
            client.RequireClientSecret = requireClientSecret;
            client.AccessTokenLifetime = accessTokenLifetime;
            client.ConsentLifetime = consentLifetime;
            client.IdentityTokenLifetime = identityTokenLifetime;
            client.AuthorizationCodeLifetime = authorizationCodeLifetime;
            client.AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime;
            client.SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime;
            client.RefreshTokenExpiration = refreshTokenExpiration;
            client.DeviceCodeLifetime = deviceCodeLifetime;
            //client.ProtocolType = protocolType;
            client.AlwaysIncludeUserClaimsInIdToken = alwaysIncludeUserClaimsInIdToken;
            client.AllowPlainTextPkce = allowPlainTextPkce;
            client.AllowOfflineAccess = allowOfflineAccess;
            client.AllowAccessTokensViaBrowser = allowAccessTokensViaBrowser;
            client.RefreshTokenUsage = refreshTokenUsage;
            client.UpdateAccessTokenClaimsOnRefresh = updateAccessTokenClaimsOnRefresh;
            client.AccessTokenType = accessTokenType;
            client.AlwaysSendClientClaims = alwaysSendClientClaims;
            client.ClientClaimsPrefix = clientClaimsPrefix;
            client.PairWiseSubjectSalt = pairWiseSubjectSalt;
            client.UserSsoLifetime = userSsoLifetime;
            client.UserCodeType = userCodeType;
            client.EnableLocalLogin = enableLocalLogin;

            if (secret.IsNotNullOrWhiteSpace())
            {
                if (client.ClientSecrets.Count == 0)
                {
                    client.AddSecret(secret.ToSha256(), null, secretType, String.Empty);
                }
                else
                {
                    if (!client.ClientSecrets.All(e => e.Value == secret && e.Type == secretType))
                    {
                        client.ClientSecrets.Clear();
                        client.AddSecret(secret.ToSha256(), null, secretType, String.Empty);
                    }

                }
            }

            client.RemoveAllAllowedGrantTypes();
            client.AddGrantType(allowGrantType);

            return await _clientRepository.UpdateAsync(client);
        }

        /// <summary>
        /// 更新client scopes
        /// </summary>
        /// <returns></returns>
        public async Task<Client> UpdateScopesAsync(string clientId, List<string> scopes)
        {
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            client.RemoveAllScopes();
            scopes.ForEach(item => { client.AddScope(item.Trim()); });

            return await _clientRepository.UpdateAsync(client);
        }

        /// <summary>
        /// 新增回调地址
        /// </summary>
        public async Task<Client> AddRedirectUriAsync(string clientId, string uri)
        {
            uri = uri.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.RedirectUris.Any(e => e.RedirectUri == uri.Trim()))
            {
                return client;
            }
            else
            {
                client.AddRedirectUri(uri);
                return await _clientRepository.UpdateAsync(client);
            }
        }

        /// <summary>
        /// 删除回调地址
        /// </summary>
        public async Task<Client> RemoveRedirectUriAsync(string clientId, string uri)
        {
            uri = uri.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.RedirectUris.Any(e => e.RedirectUri == uri.Trim()))
            {
                client.RemoveRedirectUri(uri);
                return await _clientRepository.UpdateAsync(client);
            }
            else
            {
                return client;
            }
        }

        /// <summary>
        /// 新增Logout回调地址
        /// </summary>
        public async Task<Client> AddLogoutRedirectUriAsync(string clientId, string uri)
        {
            uri = uri.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.PostLogoutRedirectUris.Any(e => e.PostLogoutRedirectUri == uri))
            {
                return client;
            }

            client.AddPostLogoutRedirectUri(uri);
            return await _clientRepository.UpdateAsync(client);
        }

        /// <summary>
        /// 删除Logout回调地址
        /// </summary>
        public async Task<Client> RemoveLogoutRedirectUriAsync(string clientId, string uri)
        {
            uri = uri.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.PostLogoutRedirectUris.Any(e => e.PostLogoutRedirectUri == uri))
            {
                client.RemovePostLogoutRedirectUri(uri);
                await _clientRepository.UpdateAsync(client);
            }

            return client;
        }

        /// <summary>
        /// 添加cors
        /// </summary>
        public async Task<Client> AddCorsAsync(string clientId, string origin)
        {
            origin = origin.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.AllowedCorsOrigins.Any(e => e.Origin == origin))
            {
                return client;
            }
            else
            {
                client.AddCorsOrigin(origin);
                return await _clientRepository.UpdateAsync(client);
            }
        }

        /// <summary>
        /// 删除cors
        /// </summary>
        public async Task<Client> RemoveCorsAsync(string clientId, string origin)
        {
            origin = origin.Trim();
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            if (client.AllowedCorsOrigins.Any(e => e.Origin == origin))
            {
                client.RemoveCorsOrigin(origin);
                return await _clientRepository.UpdateAsync(client);
            }

            return client;
        }

        public async Task<Client> EnabledAsync(string clientId, bool enabled)
        {
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client == null) throw new UserFriendlyException(message: $"{clientId}不存在");
            client.Enabled = enabled;
            return await _clientRepository.UpdateAsync(client);
        }
    }
}