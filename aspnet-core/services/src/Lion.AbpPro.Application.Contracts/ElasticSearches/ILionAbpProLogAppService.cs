using Lion.AbpPro.ElasticSearches.Dto;

namespace Lion.AbpPro.ElasticSearches
{
    public interface ILionAbpProLogAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询es日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomPagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input);
    }
}