using System.Threading.Tasks;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryController : DataDictionaryManagementController, IApplicationService
    {
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public DataDictionaryController(IDataDictionaryAppService dataDictionaryAppService)
        {
            _dataDictionaryAppService = dataDictionaryAppService;
        }

        /// <summary>
        /// 分页查询字典项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<PagedResultDto<PagingDataDictionaryOutput>> GetPagingListAsync(PagingDataDictionaryInput input)
        {
            return _dataDictionaryAppService.GetPagingListAsync(input);
        }
    }
}