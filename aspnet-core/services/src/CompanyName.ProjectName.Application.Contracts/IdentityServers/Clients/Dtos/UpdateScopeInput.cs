using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
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