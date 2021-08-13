using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.DataDictionaryManagement.MongoDB
{
    [ConnectionStringName(DataDictionaryManagementDbProperties.ConnectionStringName)]
    public class DataDictionaryManagementMongoDbContext : AbpMongoDbContext, IDataDictionaryManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDataDictionaryManagement();
        }
    }
}