using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Lion.AbpPro.Users;

public interface IBulkImportUserRepository : ITransientDependency
{
    Task BulkInsertAsync(List<IdentityUser> identityUsers);
}