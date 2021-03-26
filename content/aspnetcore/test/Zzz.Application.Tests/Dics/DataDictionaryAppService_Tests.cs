using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Zzz.Dic;
using Zzz.DTOs.Dic;

namespace Zzz.Dics
{
    public class DataDictionaryAppService_Tests: ZzzApplicationTestBase
    {
        private readonly IDicAppService _dicAppService;

        public DataDictionaryAppService_Tests()
        {
            _dicAppService = GetRequiredService<IDicAppService>();
        }

        [Fact]
        public async Task Shuold_Get_List_Of_Dics()
        {
            var result = await _dicAppService.GetListAsync("Group");
            result.Code.ShouldBe(200);
        }

        [Fact]
        public async Task Should_Create_Dic()
        {
            var dic = new CreateDataDictionaryDto() { Name = "Group01", Description = "单元测试01" };

            var result = await _dicAppService.CreateAsync(dic);

            result.Code.ShouldBe(200);
        }
    }
}
