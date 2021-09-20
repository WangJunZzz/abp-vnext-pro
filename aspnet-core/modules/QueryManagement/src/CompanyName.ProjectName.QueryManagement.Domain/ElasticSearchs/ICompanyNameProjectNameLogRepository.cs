using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
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