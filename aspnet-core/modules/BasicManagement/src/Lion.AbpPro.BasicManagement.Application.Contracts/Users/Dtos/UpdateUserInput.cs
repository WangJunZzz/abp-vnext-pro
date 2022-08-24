using Volo.Abp.Identity;

namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class UpdateUserInput
    {
        public Guid UserId { get; set; }

        public IdentityUserUpdateDto UserInfo { get; set; }
    }
}
