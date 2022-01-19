using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class EnabledInput
    {
        [Required]
        public string ClientId { get; set; }
        
        public bool Enabled { get; set; }
    }
}