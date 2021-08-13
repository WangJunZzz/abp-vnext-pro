using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.QueryManagement.MongoDB
{
    [ConnectionStringName(QueryManagementDbProperties.ConnectionStringName)]
    public class QueryManagementMongoDbContext : AbpMongoDbContext, IQueryManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureQueryManagement();
        }
    }
}