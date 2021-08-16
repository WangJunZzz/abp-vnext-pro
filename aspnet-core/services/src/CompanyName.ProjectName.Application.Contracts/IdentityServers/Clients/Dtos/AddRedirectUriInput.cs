using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class AddRedirectUriInput
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string Uri { get; set; }
    }
}