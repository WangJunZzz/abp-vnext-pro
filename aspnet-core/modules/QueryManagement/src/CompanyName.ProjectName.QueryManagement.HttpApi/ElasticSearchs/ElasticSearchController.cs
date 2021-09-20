using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
{
    [Route("api/QueryManagement/ElasticSearch")]
    public class ElasticSearchController : QueryManagementController, IApplicationService
    {
        private readonly ILogAppService _logAppService;

        public ElasticSearchController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        [HttpPost("paging")]
        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingLogAsync(PagingElasticSearchLogInput input)
        {
            return _logAppService.PaingLogAsync(input);
        }
    }
}