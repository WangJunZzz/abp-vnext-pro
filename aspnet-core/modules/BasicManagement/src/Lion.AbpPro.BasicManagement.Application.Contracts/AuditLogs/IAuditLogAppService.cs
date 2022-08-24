namespace Lion.AbpPro.BasicManagement.AuditLogs
{
    public interface IAuditLogAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询审计日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetAuditLogPageListOutput>> GetListAsync(PagingAuditLogListInput input);
    }
}