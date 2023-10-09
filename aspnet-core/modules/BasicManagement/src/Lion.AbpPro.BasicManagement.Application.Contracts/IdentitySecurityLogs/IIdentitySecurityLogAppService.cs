namespace Lion.AbpPro.BasicManagement.IdentitySecurityLogs;

public interface IIdentitySecurityLogAppService : IApplicationService
{
    /// <summary>
    /// 分页获取登录日志
    /// </summary>
    Task<PagedResultDto<PagingIdentitySecurityLogOutput>> GetListAsync(PagingIdentitySecurityLogInput input);
}