using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class EnabledInput
    {
        [Required]
        public string ClientId { get; set; }
        
        public bool Enabled { get; set; }
    }
}