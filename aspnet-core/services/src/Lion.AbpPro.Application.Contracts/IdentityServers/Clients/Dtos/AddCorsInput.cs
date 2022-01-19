using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class AddCorsInput
    {
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Origin { get; set; }
    }
}