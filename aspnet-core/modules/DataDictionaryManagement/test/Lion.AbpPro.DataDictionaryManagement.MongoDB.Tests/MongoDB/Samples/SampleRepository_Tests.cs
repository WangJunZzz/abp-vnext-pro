using Lion.AbpPro.DataDictionaryManagement.Samples;
using Xunit;

namespace Lion.AbpPro.DataDictionaryManagement.MongoDB.Samples
{
    [Collection(MongoTestCollection.Name)]
    public class SampleRepository_Tests : SampleRepository_Tests<DataDictionaryManagementMongoDbTestModule>
    {
        /* Don't write custom repository tests here, instead write to
         * the base class.
         * One exception can be some specific tests related to MongoDB.
         */
    }
}
