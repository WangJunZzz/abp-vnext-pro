using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore
{
    [ConnectionStringName(DataDictionaryManagementDbProperties.ConnectionStringName)]
    public interface IDataDictionaryManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<DataDictionary> DataDictionary { get; }
    }
}