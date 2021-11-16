using System.Threading.Tasks;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.ElasticsearchRepository
{
    public interface ICompanyNameProjectNameLogRepository : ITransientDependency
    {
        /// <summary>
        /// 分页查询es日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input);
    }
}