using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Threading;

namespace CompanyName.ProjectName.QueryManagement.FreeSqlMySql
{
    public abstract class FreeSqlBasicRepository : DomainService
    {
       
        protected IFreeSql FreeSql => LazyServiceProvider.LazyGetRequiredService<IFreeSql>();

        private ICancellationTokenProvider CancellationTokenProvider => LazyServiceProvider.LazyGetService<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);

        protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
        {
            return CancellationTokenProvider.FallbackToProvider(preferredValue);
        }
    }
}
