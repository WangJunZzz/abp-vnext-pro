using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.Users.Dtos
{
    public class PagingUserListInput : PagingBase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Filter { get; set; }
    }
}