using System;
using Volo.Abp.Identity;

namespace Lion.AbpPro.Roles.Dtos
{
    public class UpdateRoleInput
    {
        public Guid RoleId { get; set; }

        public IdentityRoleUpdateDto RoleInfo { get; set; }
    }
}