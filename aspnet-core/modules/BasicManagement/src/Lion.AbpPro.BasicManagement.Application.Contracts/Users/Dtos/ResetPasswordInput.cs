using Volo.Abp.Account.Localization;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Lion.AbpPro.BasicManagement.Users.Dtos;

public class ResetPasswordInput
{
    public Guid UserId { get; set; }
    
    [Required]
    [DisableAuditing]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
    public string Password { get; set; }
}