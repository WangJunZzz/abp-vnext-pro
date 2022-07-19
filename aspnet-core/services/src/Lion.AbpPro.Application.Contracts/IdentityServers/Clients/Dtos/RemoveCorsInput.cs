namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class RemoveCorsInput
    {
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Origin { get; set; }
    }
}