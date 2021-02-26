using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Zzz.EntityFrameworkCore;

namespace Zzz.Repository
{
    public class UserDapperRepository : DapperRepository<ZzzDbContext>, IUserDapperRepository
    {
       
        public UserDapperRepository(IDbContextProvider<ZzzDbContext> dbContextProvider) : base(dbContextProvider)
        {
           
        }

        public  async Task<List<string>> GetAllUserNameListAsync()
        {
            var connection = await GetDbConnectionAsync();
            return (await connection.QueryAsync<string>("select Name from AbpUsers")).ToList();
        }
    }
}
