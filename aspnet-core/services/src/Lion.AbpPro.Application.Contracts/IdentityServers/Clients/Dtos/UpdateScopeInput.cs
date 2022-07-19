namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class UpdateScopeInput
    {
        [Required]
        public string ClientId { get; set; }

        public List<string> Scopes { get; set; }

        public UpdateScopeInput()
        {
            Scopes = new List<string>();        
        }
    }
}