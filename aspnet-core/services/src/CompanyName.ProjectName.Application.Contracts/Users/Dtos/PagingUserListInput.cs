using CompanyName.ProjectName.Extensions.Customs;
using CompanyName.ProjectName.Publics.Dtos;

namespace CompanyName.ProjectName.Users.Dtos
{
    public class PagingUserListInput : PagingBase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Filter { get; set; }
    }
}