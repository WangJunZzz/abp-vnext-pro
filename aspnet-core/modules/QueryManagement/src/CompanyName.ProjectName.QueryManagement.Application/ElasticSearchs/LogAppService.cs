using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
{
    public class LogAppService : QueryManagementAppService, ILogAppService
    {
        private readonly ICompanyNameProjectNameLogRepository _companyNameProjectNameLogRepository;

        public LogAppService(ICompanyNameProjectNameLogRepository companyNameProjectNameLogRepository)
        {
            _companyNameProjectNameLogRepository = companyNameProjectNameLogRepository;
        }

        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingLogAsync(PagingElasticSearchLogInput input)
        {
            return _companyNameProjectNameLogRepository.PaingAsync(input);
        }
    }
}