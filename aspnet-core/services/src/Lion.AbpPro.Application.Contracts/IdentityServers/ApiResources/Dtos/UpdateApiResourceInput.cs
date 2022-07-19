namespace Lion.AbpPro.IdentityServers.ApiResources.Dtos
{
    public class UpdateApiResourceInput
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public string Secret { get; set; }

        public List<string> ApiScopes { get; set; }



        public UpdateApiResourceInput()
        {
            ApiScopes = new List<string>();
        }
    }
}