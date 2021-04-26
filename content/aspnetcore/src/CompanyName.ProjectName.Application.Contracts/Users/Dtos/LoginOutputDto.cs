using System;

namespace CompanyNameProjectName.Dtos.Users
{
    public class LoginOutputDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}
