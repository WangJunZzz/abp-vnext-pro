using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class RemoveRedirectUriInput
    {
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Uri { get; set; }
    }
}