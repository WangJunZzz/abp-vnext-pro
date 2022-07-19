namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries
{
    public interface IDataDictionaryAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingDataDictionaryOutput>> GetPagingListAsync(
            PagingDataDictionaryInput input);

        /// <summary>
        /// 分页查询字典项明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PagingDataDictionaryDetailOutput>> GetPagingDetailListAsync(
            PagingDataDictionaryDetailInput input);

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <returns></returns>
        Task CreateAsync(CreateDataDictinaryInput input);

        /// <summary>
        /// 新增字典明细
        /// </summary>
        Task CreateDetailAsync(CreateDataDictinaryDetailInput input);

        /// <summary>
        /// 设置字典明细状态
        /// </summary>
        Task SetStatus(SetDataDictinaryDetailInput input);

        Task UpdateDetailAsync(UpdateDetailInput input);

        /// <summary>
        /// 删除数据字典明细项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(DeleteDataDictionaryDetailInput input);

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDictinaryTypeAsync(IdInput input);

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateDataDictinaryInput input);

    }
}