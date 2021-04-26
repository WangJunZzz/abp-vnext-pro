using System.ComponentModel.DataAnnotations;
using Volo.Abp.PermissionManagement;

namespace CompanyNameProjectName.Roles.Dtos
{
    public class UpdateRolePermissionsDto
    {
        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string ProviderKey { get; set; }

        public UpdatePermissionsDto UpdatePermissionsDto { get; set; }
    }
}
