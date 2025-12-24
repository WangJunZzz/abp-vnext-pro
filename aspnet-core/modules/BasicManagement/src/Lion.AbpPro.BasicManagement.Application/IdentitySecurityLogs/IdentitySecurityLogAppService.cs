using Mapster;

namespace Lion.AbpPro.BasicManagement.IdentitySecurityLogs;

public class IdentitySecurityLogAppService : BasicManagementAppService, IIdentitySecurityLogAppService
{
    private readonly IIdentitySecurityLogRepository _identitySecurityLogRepository;

    public IdentitySecurityLogAppService(IIdentitySecurityLogRepository identitySecurityLogRepository)
    {
        _identitySecurityLogRepository = identitySecurityLogRepository;
    }
    
    [Authorize(Policy = BasicManagementPermissions.SystemManagement.IdentitySecurityLog)]
    public virtual async Task<PagedResultDto<PagingIdentitySecurityLogOutput>> GetListAsync(PagingIdentitySecurityLogInput input)
    {
        var totalCount = await _identitySecurityLogRepository.GetCountAsync(
            input.StartTime,
            input.EndTime, 
            input.ApplicationName,
            input.Identity,
            input.Action,
            input.UserId,
            input.UserName,
            input.ClientId,
            input.CorrelationId);
        if (totalCount == 0)
        {
            return new PagedResultDto<PagingIdentitySecurityLogOutput>();
        }

        var list = await _identitySecurityLogRepository.GetListAsync(
            input.Sorting,
            input.PageSize,
            input.SkipCount,
            input.StartTime,
            input.EndTime, 
            input.ApplicationName,
            input.Identity,
            input.Action,
            input.UserId,
            input.UserName,
            input.ClientId,
            input.CorrelationId);

        return new PagedResultDto<PagingIdentitySecurityLogOutput>(totalCount,list.Adapt<List<PagingIdentitySecurityLogOutput>>());
    }
}