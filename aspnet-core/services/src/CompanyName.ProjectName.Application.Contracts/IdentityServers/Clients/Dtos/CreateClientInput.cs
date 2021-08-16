using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class CreateClientInput
    {
        [Required] public string ClientId { get; set; }

        [Required] public string ClientName { get; set; }

        public string Description { get; set; }
    }
}