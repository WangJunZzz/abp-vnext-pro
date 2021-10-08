using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace CompanyName.ProjectName.IdentityServer
{
    public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public IdentityServerDataSeedContributor(
            IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IApiScopeRepository apiScopeRepository,
            IIdentityResourceDataSeeder identityResourceDataSeeder,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder,
            IConfiguration configuration,
            ICurrentTenant currentTenant)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                await _identityResourceDataSeeder.CreateStandardResourcesAsync();
                // await CreateApiResourcesAsync();
                // await CreateApiScopesAsync();
                await CreateClientsAsync();
            }
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync();
        }

        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

            await CreateApiResourceAsync(commonApiUserClaims);
        }

        private async Task<ApiResource> CreateApiResourceAsync(IEnumerable<string> claims)
        {
            var apiResource = new ApiResource(
                _guidGenerator.Create(),
                "Identity.Api",
                "身份认证中心Api"
            );

            //foreach (var claim in claims)
            //{
            //    if (apiResource.FindClaim(claim) == null)
            //    {
            //        apiResource.AddUserClaim(claim);
            //    }
            //}


            return await _apiResourceRepository.InsertAsync(apiResource);
        }

        private async Task CreateApiScopeAsync()
        {
            await _apiScopeRepository.InsertAsync(
                new ApiScope(
                    _guidGenerator.Create(),
                    "Api_Read"
                ),
                autoSave: true
            );


            await _apiScopeRepository.InsertAsync(
                new ApiScope(
                    _guidGenerator.Create(),
                    "Api_Write"
                ),
                autoSave: true
            );
        }

        private async Task CreateClientsAsync()
        {
            var client = await _clientRepository.FindByClientIdAsync("Vue3");
            if (client != null)
            {
                return;
            }

            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address",
            };

            await CreateClientAsync(
                name: "Vue3",
                description: "Vue3-UI",
                scopes: commonScopes,
                grantTypes: new[] {"implicit"},
                secret: "1q2w3E*".Sha256(),
                redirectUri: "http://localhost:4200/oidcSignIn",
                postLogoutRedirectUri: "http://localhost:4200/oidcSignOut",
                frontChannelLogoutUri: "http://localhost:4200/oidcSignOut",
                corsOrigins: new[] {"https://localhost:4200", "http://localhost:4200", "http://120.24.194.14:8012"},
                requireClientSecret: false
            );
        }

        private async Task<Client> CreateClientAsync(
            string name,
            string description,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret = null,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            string frontChannelLogoutUri = null,
            bool requireClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> permissions = null,
            IEnumerable<string> corsOrigins = null)
        {
            var client = new Client(
                _guidGenerator.Create(),
                name
            )
            {
                ClientName = name,
                ProtocolType = "oidc",
                Description = description,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                AbsoluteRefreshTokenLifetime = 31536000, //365 days
                AccessTokenLifetime = 31536000, //365 days
                AuthorizationCodeLifetime = 300,
                IdentityTokenLifetime = 300,
                RequireConsent = false,
                FrontChannelLogoutUri = frontChannelLogoutUri,
                RequireClientSecret = requireClientSecret,
                RequirePkce = requirePkce,
                AllowAccessTokensViaBrowser = true,
            };


            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions,
                    null
                );
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.InsertAsync(client);
        }
    }
}