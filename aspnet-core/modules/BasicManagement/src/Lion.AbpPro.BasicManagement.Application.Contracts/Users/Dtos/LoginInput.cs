using Volo.Abp.Auditing;

namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginInput : IValidatableObject
    {
        /// <summary>
        /// 用户名或者邮箱
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [DisableAuditing]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult("Email can not be null", new[] { "Email" });
            }

            if (Password.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult("Password can not be null", new[] { "Password" });
            }
        }
    }
}
