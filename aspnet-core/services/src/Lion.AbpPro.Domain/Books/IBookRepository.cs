using Volo.Abp.Domain.Repositories;

namespace Lion.AbpPro.Books;

public interface IBookRepository : IBasicRepository<Book, Guid>
{
    Task<List<Book>> GetListAsync(DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0);

    Task<long> GetCountAsync(DateTime? startDateTime = null, DateTime? endDateTime = null);
}