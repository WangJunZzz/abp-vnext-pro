using AutoMapper;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using CompanyName.ProjectName.ElasticSearchs;
using CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos;
using CompanyName.ProjectName.IdentityServers.Clients;
using CompanyName.ProjectName.IdentityServers.Dtos;
using CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.IdentityResources;

namespace CompanyName.ProjectName.IdentityServers.Mappers
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