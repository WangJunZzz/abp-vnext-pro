using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.IdentityServer;
using CompanyName.ProjectName.Publics.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.Clients;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class IdentityServerClientAppService : ProjectNameAppService, IIdentityServerClientAppService
    {
        private readonly IdenityServerClientManager _idenityServerClientManager;

        public IdentityServerClientAppService(IdenityServerClientManager idenityServerClientManager)
        {
            _idenityServerClientManager = idenityServerClientManager;
        }


        public async Task<PagedResultDto<PagingClientListOutput>> GetListAsync(PagingClientListInput input)
        {
            var list = await _idenityServerClientManager.GetListAsync(
                input.SkipCount,
                input.PageSize,
                input.Filter,
                true);
            var totalCount = await _idenityServerClientManager.GetCountAsync(input.Filter);
            return new PagedResultDto<PagingClientListOutput>(totalCount,
                ObjectMapper.Map<List<Client>, List<PagingClientListOutput>>(list));
        }

        /// <summary>
        /// 创建Client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task CreateAsync(CreateClientInput input)
        {
            return _idenityServerClientManager.CreateAsync(input.ClientId, input.ClientName, input.Description);
        }

        /// <summary>
        /// 删除client
        /// </summary>
        /// <returns></returns>
        public Task DeleteAsync(IdInput input)
        {
            return _idenityServerClientManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <returns></returns>
        public Task UpdateBasicDataAsync(UpdataBasicDataInput input)
        {
            return _idenityServerClientManager.UpdateBasicDataAsync(
                input.ClientId,
                input.ClientName,
                input.Description,
                input.ClientUri,
                input.LogoUri,
                input.Enabled,
                input.ProtocolType,
                input.RequireClientSecret,
                input.RequireConsent,
                input.AllowRememberConsent,
                input.AlwaysIncludeUserClaimsInIdToken,
                input.RequirePkce,
                input.AllowPlainTextPkce,
                input.RequireRequestObject,
                input.AllowAccessTokensViaBrowser,
                input.FrontChannelLogoutUri,
                input.FrontChannelLogoutSessionRequired,
                input.BackChannelLogoutUri,
                input.BackChannelLogoutSessionRequired,
                input.AllowOfflineAccess,
                input.IdentityTokenLifetime,
                input.AllowedIdentityTokenSigningAlgorithms,
                input.AccessTokenLifetime,
                input.AuthorizationCodeLifetime,
                input.ConsentLifetime,
                input.AbsoluteRefreshTokenLifetime,
                input.RefreshTokenUsage,
                input.UpdateAccessTokenClaimsOnRefresh,
                input.RefreshTokenExpiration,
                input.AccessTokenType,
                input.EnableLocalLogin,
                input.IncludeJwtId,
                input.AlwaysSendClientClaims,
                input.ClientClaimsPrefix,
                input.PairWiseSubjectSalt,
                input.UserSsoLifetime,
                input.UserCodeType,
                input.DeviceCodeLifetime,
                input.SlidingRefreshTokenLifetime,
                input.Secret,
                input.SecretType
            );
        }

        /// <summary>
        /// 更新client scopes
        /// </summary>
        /// <returns></returns>
        public Task UpdateScopesAsync(UpdateScopeInput input)
        {
            return _idenityServerClientManager.UpdateScopesAsync(input.ClientId, input.Scopes);
        }

        /// <summary>
        /// 新增回调地址
        /// </summary>
        public Task AddRedirectUriAsync(AddRedirectUriInput input)
        {
            return _idenityServerClientManager.AddRedirectUriAsync(input.ClientId, input.Uri);
        }

        /// <summary>
        /// 删除回调地址
        /// </summary>
        public Task RemoveRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _idenityServerClientManager.RemoveRedirectUriAsync(input.ClientId, input.Uri);
        }

        /// <summary>
        /// 新增Logout回调地址
        /// </summary>
        public Task AddLogoutRedirectUriAsync(AddRedirectUriInput input)
        {
            return _idenityServerClientManager.AddLogoutRedirectUriAsync(input.ClientId, input.Uri);
        }

        /// <summary>
        /// 删除Logout回调地址
        /// </summary>
        public Task RemoveLogoutRedirectUriAsync(RemoveRedirectUriInput input)
        {
            return _idenityServerClientManager.RemoveLogoutRedirectUriAsync(input.ClientId, input.Uri);
        }

        /// <summary>
        /// 添加cors
        /// </summary>
        public Task AddCorsAsync(AddCorsInput input)
        {
            return _idenityServerClientManager.AddCorsAsync(input.ClientId, input.Origin);
        }

        /// <summary>
        /// 删除cors
        /// </summary>
        public Task RemoveCorsAsync(RemoveCorsInput input)
        {
            return _idenityServerClientManager.RemoveCorsAsync(input.ClientId, input.Origin);
        }

        /// <summary>
        /// 禁用client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task EnabledAsync(EnabledInput input)
        {
            return _idenityServerClientManager.EnabledAsync(input.ClientId, input.Enabled);
        }
    }
}