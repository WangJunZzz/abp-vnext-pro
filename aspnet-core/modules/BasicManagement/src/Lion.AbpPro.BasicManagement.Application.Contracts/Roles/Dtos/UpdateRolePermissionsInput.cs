namespace Lion.AbpPro.BasicManagement.Roles.Dtos
{
    public class UpdateRolePermissionsInput
    {
        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string ProviderKey { get; set; }

        public UpdatePermissionsDto UpdatePermissionsDto { get; set; }
    }
}