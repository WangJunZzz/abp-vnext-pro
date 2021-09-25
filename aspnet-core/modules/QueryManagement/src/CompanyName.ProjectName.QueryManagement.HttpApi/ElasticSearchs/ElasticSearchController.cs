using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using CompanyName.ProjectName.QueryManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs
{
    [Route("api/QueryManagement/ElasticSearch")]
    [Authorize(Policy = IdentityPermissions.Users.Default)]
    public class ElasticSearchController : QueryManagementController, IApplicationService
    {
        private readonly ILogAppService _logAppService;

        public ElasticSearchController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        [HttpPost("paging")]
        [Authorize(Policy = QueryManagementPermissions.SystemManagement.ES)]
        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingLogAsync(PagingElasticSearchLogInput input)
        {
            return _logAppService.PaingLogAsync(input);
        }
    }
}