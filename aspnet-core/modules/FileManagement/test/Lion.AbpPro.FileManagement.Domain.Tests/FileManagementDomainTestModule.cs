using Lion.AbpPro.FileManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.FileManagement;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FileManagementEntityFrameworkCoreTestModule)
)]
public class FileManagementDomainTestModule : AbpModule
{
}