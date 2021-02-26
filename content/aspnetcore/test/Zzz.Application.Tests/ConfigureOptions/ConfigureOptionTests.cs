using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzz.Options;
using Shouldly;
using Xunit;

namespace Zzz.ConfigureOptions
{
    public class ConfigureOptionTests : ZzzApplicationTestBase
    {
        private readonly JwtOptions jwtOptions;
        public ConfigureOptionTests()
        {
            jwtOptions = GetRequiredService<IOptions<JwtOptions>>().Value;
        }

        [Fact]
        void Get_Options()
        {
            jwtOptions.Audience.ShouldNotBeNull();

        }
    }
}
