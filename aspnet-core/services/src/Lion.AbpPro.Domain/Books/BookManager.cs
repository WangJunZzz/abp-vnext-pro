using Volo.Abp;

namespace Lion.AbpPro.Books;

public class BookManager : DomainService
{
    private readonly IBookRepository _bookRepository;
    private readonly IObjectMapper _objectMapper;

    public BookManager(IBookRepository bookRepository, IObjectMapper objectMapper)
    {
        _bookRepository = bookRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<BookDto>> GetListAsync(DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0)
    {
        var list = await _bookRepository.GetListAsync(startDateTime, endDateTime, maxResultCount, skipCount);
        return _objectMapper.Map<List<Book>, List<BookDto>>(list);
    }

    public async Task<long> GetCountAsync(DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return await _bookRepository.GetCountAsync(startDateTime, endDateTime);
    }

    /// <summary>
    /// 创建书籍
    /// </summary>
    public async Task<BookDto> CreateAsync(
        Guid id,
        string no,
        string name,
        decimal price,
        string remark,
        BookType bookType
        )
    {   
        var entity = new Book(id, no, name, price, remark, bookType);
        entity = await _bookRepository.InsertAsync(entity);
        return _objectMapper.Map<Book, BookDto>(entity);            
    }   

    /// <summary>
    /// 更新书籍
    /// </summary>
    public async Task<BookDto> UpdateAsync(
        Guid id,
        string no,
        string name,
        decimal price,
        string remark,
        BookType bookType
        )
    {
        var entity = await _bookRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException($"书籍不存在");
        entity.Update(no, name, price, remark, bookType);
        entity = await _bookRepository.UpdateAsync(entity);
        return _objectMapper.Map<Book, BookDto>(entity);            
    }     

    /// <summary>
    /// 删除书籍
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _bookRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException($"书籍不存在");
        await _bookRepository.DeleteAsync(entity);          
    }
    
}