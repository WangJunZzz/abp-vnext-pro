using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using Microsoft.Extensions.Configuration;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
{
    public class LogAppService : QueryManagementAppService, ILogAppService
    {
        private readonly ICompanyNameProjectNameLogRepository _companyNameProjectNameLogRepository;
        private readonly IConfiguration _configuration;

        public LogAppService(
            ICompanyNameProjectNameLogRepository companyNameProjectNameLogRepository,
            IConfiguration configuration)
        {
            _companyNameProjectNameLogRepository = companyNameProjectNameLogRepository;
            _configuration = configuration;
        }

        public async Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingLogAsync(PagingElasticSearchLogInput input)
        {
            var enabled = _configuration.GetValue<bool>("LogToElasticSearch:Enabled", false);
            if (enabled)
            {
                return await _companyNameProjectNameLogRepository.PaingAsync(input);
            }
            else
            {
                return new CustomePagedResultDto<PagingElasticSearchLogOutput>(0, new List<PagingElasticSearchLogOutput>());
            }
        }
    }
}