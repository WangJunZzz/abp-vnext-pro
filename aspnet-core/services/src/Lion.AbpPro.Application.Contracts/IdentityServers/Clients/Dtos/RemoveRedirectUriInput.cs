using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.IdentityServers.Clients
{
    public class RemoveRedirectUriInput
    {
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Uri { get; set; }
    }
}