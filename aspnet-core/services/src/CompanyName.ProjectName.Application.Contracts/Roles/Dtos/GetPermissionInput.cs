using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Roles.Dtos
{
    public class GetPermissionInput
    {
        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string ProviderKey { get; set; }
    }
}