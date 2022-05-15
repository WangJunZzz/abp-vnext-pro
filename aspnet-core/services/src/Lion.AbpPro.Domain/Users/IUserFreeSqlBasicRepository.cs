using Lion.AbpPro.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion.AbpPro.Users
{
    public interface IUserFreeSqlBasicRepository
    {
        Task<List<UserOutput>> GetListAsync();
    }
}
