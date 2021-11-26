using System.Threading.Tasks;
using Lion.AbpPro.ElasticsearchRepository.Dto;
using Lion.AbpPro.ElasticSearchs;
using Lion.AbpPro.Permissions;
using Lion.AbpPro.Extension.Customs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lion.AbpPro.Controllers.Systems
{
    [Route("EsLog")]
    public class LionAbpProLogController: AbpProController,ILionAbpProLogAppService
    {
        private readonly ILionAbpProLogAppService _companyNameAbpProLogAppService;

        public LionAbpProLogController(ILionAbpProLogAppService companyNameAbpProLogAppService)
        {
            _companyNameAbpProLogAppService = companyNameAbpProLogAppService;
        }
        
        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取Es日志", Tags = new[] { "EsLog" })]
        public Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            return _companyNameAbpProLogAppService.PaingAsync(input);
        }
    }
}