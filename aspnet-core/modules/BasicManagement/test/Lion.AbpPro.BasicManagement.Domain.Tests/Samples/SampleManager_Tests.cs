using System.Threading.Tasks;
using Xunit;

namespace Lion.AbpPro.BasicManagement.Samples;

public class SampleManager_Tests : BasicManagementDomainTestBase
{
    //private readonly SampleManager _sampleManager;

    public SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public async Task Method1Async()
    {
        await Task.CompletedTask;
    }
}