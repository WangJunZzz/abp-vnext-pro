using Magicodes.ExporterAndImporter.Core;

namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class ExportIdentityUserOutput
    {
        [ExporterHeader(DisplayName = "用户名")] 
        public string UserName { get; set; }

        [ExporterHeader(DisplayName = "真实名称")] 
        public string Name { get; set; }

        [ExporterHeader(DisplayName = "邮箱")] 
        public string Email { get; set; }

        [ExporterHeader(DisplayName = "手机号码")] 
        public string PhoneNumber { get; set; }

        [ExporterHeader(IsIgnore = true)] 
        public bool IsActive { get; set; }


        [ExporterHeader(DisplayName = "状态")] 
        public string Status => IsActive ? "启用" : "禁用";

        [ExporterHeader(IsIgnore = true)] 
        public DateTime CreationTime { get; set; }

        [ExporterHeader(DisplayName = "创建时间")]
        public string CreationTimeFormat => CreationTime.ToString("yyyy-MM-dd hh:mm:ss");
    }
}