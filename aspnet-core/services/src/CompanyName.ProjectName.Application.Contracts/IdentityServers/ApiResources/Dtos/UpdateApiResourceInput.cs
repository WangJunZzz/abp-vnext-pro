using System.Collections.Generic;

namespace CompanyName.ProjectName.IdentityServers.Dtos
{
    public class UpdateApiResourceInput
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public List<ApiResourceSecretOutput> Secrets { get; set; }

        public List<ApiResourceScopeOutput> Scopes { get; set; }

        public List<ApiResourceClaimOutput> UserClaims { get; set; }

        public List<ApiResourcePropertyOutput> Properties { get; set; }

        public UpdateApiResourceInput()
        {
            Secrets = new List<ApiResourceSecretOutput>();
            Scopes = new List<ApiResourceScopeOutput>();
            UserClaims = new List<ApiResourceClaimOutput>();
            Properties = new List<ApiResourcePropertyOutput>();
        }
    }
}