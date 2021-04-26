using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyNameProjectName.Options
{
    public class JwtOptions
    {
        public int ExpirationTime { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public string Issuer { get; set; }
    }
}
