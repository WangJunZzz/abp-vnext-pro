using AutoMapper;
using Lion.AbpPro.IdentityServers.ApiResources.Dtos;
using Lion.AbpPro.IdentityServers.ApiScopes.Dtos;
using Lion.AbpPro.IdentityServers.Clients.Dtos;
using Lion.AbpPro.IdentityServers.IdentityResources.Dtos;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Lion.AbpPro.IdentityServers
{
    public class IdentityServerApplicationAutoMapperProfile : Profile
    {
        public IdentityServerApplicationAutoMapperProfile()
        {
            CreateMap<ApiResource, ApiResourceOutput>();
            CreateMap<ApiResourceClaim, ApiResourceClaimOutput>();
            CreateMap<ApiResourceProperty, ApiResourcePropertyOutput>();
            CreateMap<ApiResourceSecret, ApiResourceSecretOutput>();
            CreateMap<ApiResourceScope, ApiResourceScopeOutput>();

            CreateMap<Client, PagingClientListOutput>();
            CreateMap<ClientClaim, ClientClaimOutput>();
            CreateMap<ClientCorsOrigin, ClientCorsOriginOutput>();
            CreateMap<ClientGrantType, ClientGrantTypeOutput>();
            CreateMap<ClientIdPRestriction, ClientIdPRestrictionOutput>();
            CreateMap<ClientPostLogoutRedirectUri, ClientPostLogoutRedirectUriOutput>();
            CreateMap<ClientProperty, ClientPropertyOutput>();
            CreateMap<ClientRedirectUri, ClientRedirectUriOutput>();
            CreateMap<ClientScope, ClientScopeOutput>();
            CreateMap<ClientSecret, ClientSecretOutput>();


            CreateMap<ApiScope, PagingApiScopeListOutput>();
            CreateMap<IdentityResource, PagingIdentityResourceListOutput>();

        }
    }
}