namespace Lion.AbpPro.BasicManagement.Users.Dtos
{
    public class PagingUserListInput : PagingBase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Filter { get; set; }
    }
}