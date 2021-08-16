using AutoMapper;
using CompanyName.ProjectName.IdentityServers.Clients;
using CompanyName.ProjectName.IdentityServers.Dtos;
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
            #region id4 model to Output

            CreateMap<ApiResource, ApiResourceOutput>();
            CreateMap<ApiResourceClaim, ApiResourceClaimOutput>();
            CreateMap<ApiResourceProperty, ApiResourcePropertyOutput>();
            CreateMap<ApiResourceSecret, ApiResourceSecretOutput>();
            CreateMap<ApiResourceScope, ApiResourceScopeOutput>();
         
            CreateMap<Client, ClientOutput>();
            CreateMap<ClientClaim, ClientClaimOutput>();
            CreateMap<ClientCorsOrigin, ClientCorsOriginOutput>();
            CreateMap<ClientGrantType, ClientGrantTypeOutput>();
            CreateMap<ClientIdPRestriction, ClientIdPRestrictionOutput>();
            CreateMap<ClientPostLogoutRedirectUri, ClientPostLogoutRedirectUriOutput>();
            CreateMap<ClientProperty, ClientPropertyOutput>();
            CreateMap<ClientRedirectUri, ClientRedirectUriOutput>();
            CreateMap<ClientScope, ClientScopeOutput>();
            CreateMap<ClientSecret, ClientSecretOutput>();

            // CreateMap<DeviceFlowCodes, DeviceFlowCodesOutput>();
            // CreateMap<DeviceFlowCodes, DeviceFlowCodesOutput>();
            //
            // CreateMap<IdentityResourceClaim, IdentityResourceClaimOutput>();
            // CreateMap<IdentityResource, IdentityResourceOutput>();
            // CreateMap<IdentityResourceProperty, IdentityResourcePropertyOutput>();
            //
            //
            // CreateMap<ApiScope, ApiScopeOutput>();
            // CreateMap<ApiScopeClaim, ApiScopeClaimOutput>();
            // CreateMap<ApiScopeProperty, ApiScopePropertyOutput>();

            #endregion
        }
    }
}