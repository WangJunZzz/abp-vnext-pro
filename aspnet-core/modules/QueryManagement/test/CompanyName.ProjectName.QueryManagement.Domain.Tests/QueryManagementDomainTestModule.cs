using CompanyName.ProjectName.QueryManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(QueryManagementEntityFrameworkCoreTestModule)
        )]
    public class QueryManagementDomainTestModule : AbpModule
    {
        
    }
}
