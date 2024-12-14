using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Books;

/// <summary>
/// 书籍
/// </summary>
public interface IBookAppService : IApplicationService
{
    /// <summary>
    /// 分页查询书籍
    /// </summary>
    Task<PagedResultDto<PageBookOutput>> PageAsync(PageBookInput input);

    /// <summary>
    /// 创建书籍
    /// </summary>    
    Task CreateAsync(CreateBookInput input);

    /// <summary>
    /// 编辑书籍
    /// </summary>
    Task UpdateAsync(UpdateBookInput input);

    /// <summary>
    /// 删除书籍
    /// </summary>
    Task DeleteAsync(DeleteBookInput input);
}