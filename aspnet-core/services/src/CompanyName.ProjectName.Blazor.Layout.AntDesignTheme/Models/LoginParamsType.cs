using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Models
{
    public class LoginParamsType
    {
        [Required] public string UserName { get; set; }

        [Required] public string Password { get; set; }

        public string Mobile { get; set; }

        public string Captcha { get; set; }

        public string LoginType { get; set; }

        public bool AutoLogin { get; set; }
    }
}
