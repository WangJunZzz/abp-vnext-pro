using System.Threading.Tasks;
using CompanyName.ProjectName.ElasticsearchRepository;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using CompanyName.ProjectName.Extension.Customs.Dtos;
using CompanyName.ProjectName.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace CompanyName.ProjectName.ElasticSearchs
{
    [Authorize(Policy = ProjectNamePermissions.SystemManagement.ES)]
    public class CompanyNameProjectNameLogAppService:ProjectNameAppService,ICompanyNameProjectNameLogAppService
    {
        private readonly ICompanyNameProjectNameLogRepository _companyNameProjectNameLogRepository;

        public CompanyNameProjectNameLogAppService(ICompanyNameProjectNameLogRepository companyNameProjectNameLogRepository)
        {
            _companyNameProjectNameLogRepository = companyNameProjectNameLogRepository;
        }

        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            return _companyNameProjectNameLogRepository.PaingAsync(input);
        }
    }
}