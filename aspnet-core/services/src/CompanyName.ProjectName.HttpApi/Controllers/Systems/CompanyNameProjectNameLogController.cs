using System.Threading.Tasks;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using CompanyName.ProjectName.ElasticSearchs;
using CompanyName.ProjectName.Permissions;
using Lion.Abp.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("EsLog")]
    public class CompanyNameProjectNameLogController: ProjectNameController,ICompanyNameProjectNameLogAppService
    {
        private readonly ICompanyNameProjectNameLogAppService _companyNameProjectNameLogAppService;

        public CompanyNameProjectNameLogController(ICompanyNameProjectNameLogAppService companyNameProjectNameLogAppService)
        {
            _companyNameProjectNameLogAppService = companyNameProjectNameLogAppService;
        }
        
        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取Es日志", Tags = new[] { "EsLog" })]
        [Authorize(Policy = ProjectNamePermissions.SystemManagement.ES)]
        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            return _companyNameProjectNameLogAppService.PaingAsync(input);
        }
    }
}