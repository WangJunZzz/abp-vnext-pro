using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Zzz.Cache
{
    public class CacheTests : ZzzApplicationTestBase
    {
        ICacheManger cacheManger;
        public CacheTests()
        {
            cacheManger = GetRequiredService<ICacheManger>();
        }

        [Fact]
        async Task Shuold_Set_OK()
        {
            await cacheManger.SetAsync("test5", "value5");
            var result = await cacheManger.GetAsync("test5");
            result.ShouldBe("value");
        }

        [Fact]
        async Task Shuold_SetObject_Ok()
        {
            var info = new RedisTestDto() { Id = 1, Name = "wangjun" };
            await cacheManger.SetAsync<RedisTestDto>(nameof(RedisTestDto), info);
            var result = await cacheManger.GetAsync<RedisTestDto>(nameof(RedisTestDto));
            result.Id.ShouldBe(1);
        }
    }


    public class RedisTestDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
