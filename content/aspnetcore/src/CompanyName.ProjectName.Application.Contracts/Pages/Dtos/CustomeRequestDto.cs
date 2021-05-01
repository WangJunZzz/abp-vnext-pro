namespace CompanyNameProjectName.Pages.Dtos
{
    public class CustomeRequestDto 
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
