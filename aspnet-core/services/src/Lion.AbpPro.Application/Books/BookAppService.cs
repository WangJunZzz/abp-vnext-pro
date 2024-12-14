using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Books;

/// <summary>
/// 书籍
/// </summary>
public class BookAppService : ApplicationService, IBookAppService
{
    private readonly BookManager _bookManager;

    public BookAppService(BookManager bookManager)
    {
        _bookManager = bookManager;
    }

    /// <summary>
    /// 分页查询书籍
    /// </summary>      
    public async Task<PagedResultDto<PageBookOutput>> PageAsync(PageBookInput input)
    {
        var result = new PagedResultDto<PageBookOutput>();
        var totalCount = await _bookManager.GetCountAsync(input.StartCreationTime, input.EndCreationTime);
        result.TotalCount = totalCount;
        if (totalCount <= 0) return result;
        var list = await _bookManager.GetListAsync(input.StartCreationTime, input.EndCreationTime, input.PageSize, input.SkipCount);
        result.Items = ObjectMapper.Map<List<BookDto>, List<PageBookOutput>>(list);
        return result;
    }  

    /// <summary>
    /// 创建书籍
    /// </summary>
    public Task CreateAsync(CreateBookInput input)
    {
        return _bookManager.CreateAsync(
            GuidGenerator.Create(),
            input.No,
            input.Name,
            input.Price,
            input.Remark,
            input.BookType
			);
    }

    /// <summary>
    /// 编辑书籍
    /// </summary>
    public Task UpdateAsync(UpdateBookInput input)
    { 
        return _bookManager.UpdateAsync(
            input.Id,
            input.No,
            input.Name,
            input.Price,
            input.Remark,
            input.BookType
            );
    }

    /// <summary>
    /// 删除书籍
    /// </summary>
    public Task DeleteAsync(DeleteBookInput input)
    {
        return _bookManager.DeleteAsync(input.Id);
    }          
}