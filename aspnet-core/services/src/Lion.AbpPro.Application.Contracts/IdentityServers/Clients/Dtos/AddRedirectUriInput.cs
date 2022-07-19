namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class AddRedirectUriInput
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string Uri { get; set; }
    }
}