using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
{
    public interface ILogAppService : IApplicationService
    {
        Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingLogAsync(PagingElasticSearchLogInput input);
    }
}