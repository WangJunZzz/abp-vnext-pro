using System.Threading.Tasks;
using Lion.AbpPro.ElasticsearchRepository.Dto;
using Lion.AbpPro.Extension.Customs.Dtos;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.ElasticSearchs
{
    public interface ILionAbpProLogAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询es日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input);
    }
}