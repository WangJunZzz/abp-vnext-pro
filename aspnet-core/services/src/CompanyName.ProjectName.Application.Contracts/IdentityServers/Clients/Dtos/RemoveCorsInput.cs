using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class RemoveCorsInput
    {
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Origin { get; set; }
    }
}