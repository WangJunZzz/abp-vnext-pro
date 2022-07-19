namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    [Route("DataDictionary")]
    public class DataDictionaryController : DataDictionaryManagementController, IDataDictionaryAppService
    {
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public DataDictionaryController(IDataDictionaryAppService dataDictionaryAppService)
        {
            _dataDictionaryAppService = dataDictionaryAppService;
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页字典类型", Tags = new[] { "DataDictionary" })]
        public  Task<PagedResultDto<PagingDataDictionaryOutput>> GetPagingListAsync(
            PagingDataDictionaryInput input)
        {
            return _dataDictionaryAppService.GetPagingListAsync(input);
        }

        [HttpPost("pageDetail")]
        [SwaggerOperation(summary: "分页字典明细", Tags = new[] { "DataDictionary" })]
        public  Task<PagedResultDto<PagingDataDictionaryDetailOutput>> GetPagingDetailListAsync(
            PagingDataDictionaryDetailInput input)
        {
            return _dataDictionaryAppService.GetPagingDetailListAsync(input);
        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "创建字典类型", Tags = new[] { "DataDictionary" })]
        public Task CreateAsync(CreateDataDictinaryInput input)
        {
            return _dataDictionaryAppService.CreateAsync(input);
        }

        [HttpPost("createDetail")]
        [SwaggerOperation(summary: "创建字典明细", Tags = new[] { "DataDictionary" })]
        public  Task CreateDetailAsync(CreateDataDictinaryDetailInput input)
        {
            return _dataDictionaryAppService.CreateDetailAsync(input);
        }
        
        [HttpPost("status")]
        [SwaggerOperation(summary: "设置字典明细状态", Tags = new[] { "DataDictionary" })]
        public Task SetStatus(SetDataDictinaryDetailInput input)
        {
            return _dataDictionaryAppService.SetStatus(input);
        }

        [HttpPost("updateDetail")]
        [SwaggerOperation(summary: "更新字典明细", Tags = new[] { "DataDictionary" })]
        public Task UpdateDetailAsync(UpdateDetailInput input)
        {
            return _dataDictionaryAppService.UpdateDetailAsync(input);
        }

        [HttpPost("delete")]
        [SwaggerOperation(summary: "删除字典明细", Tags = new[] { "DataDictionary" })]
        public Task DeleteAsync(DeleteDataDictionaryDetailInput input)
        {
            return _dataDictionaryAppService.DeleteAsync(input);
        }

        [HttpPost("deleteDictinaryType")]
        [SwaggerOperation(summary: "删除字典类型", Tags = new[] { "DataDictionary" })]
        public Task DeleteDictinaryTypeAsync(IdInput input)
        {
            return _dataDictionaryAppService.DeleteDictinaryTypeAsync(input);
        }


        [HttpPost("update")]
        [SwaggerOperation(summary: "修改字典类型", Tags = new[] { "DataDictionary" })]
        public Task UpdateAsync(UpdateDataDictinaryInput input)
        {
            return _dataDictionaryAppService.UpdateAsync(input);
        }

    }
}