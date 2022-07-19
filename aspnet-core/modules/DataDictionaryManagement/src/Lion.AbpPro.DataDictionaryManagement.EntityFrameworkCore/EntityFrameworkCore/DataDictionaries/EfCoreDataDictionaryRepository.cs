namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.DataDictionaries
{
    public class EfCoreDataDictionaryRepository :
        EfCoreRepository<IDataDictionaryManagementDbContext, DataDictionary, Guid>,
        IDataDictionaryRepository
    {
        public EfCoreDataDictionaryRepository(IDbContextProvider<IDataDictionaryManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DataDictionary> FindByIdAsync(
            Guid id,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .OrderBy(t => t.CreationTime)
                .FirstOrDefaultAsync(t => t.Id == id, GetCancellationToken(cancellationToken));
        }

        public async Task<DataDictionary> FindByCodeAsync(
            string code,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .OrderBy(t => t.CreationTime)
                .FirstOrDefaultAsync(t => t.Code == code, GetCancellationToken(cancellationToken));
        }

        public async Task<DataDictionary> FindByDisplayTextAsync(
            string displayText,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .OrderBy(t => t.CreationTime)
                .FirstOrDefaultAsync(t => t.DisplayText == displayText, GetCancellationToken(cancellationToken));
        }

        public async Task<List<DataDictionary>> GetPagingListAsync(
            string filter = null,
            int maxResultCount = 10,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    e => (e.Code.Contains(filter) || e.DisplayText.Contains(filter)))
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetPagingCountAsync(string filter = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    e => (e.Code.Contains(filter) || e.DisplayText.Contains(filter)))
                .CountAsync(cancellationToken: cancellationToken);
        }
        
        public override async Task<IQueryable<DataDictionary>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}