using CompanyNameProjectName.EntityFrameworkCore;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyNameProjectName.Repository
{
    public class UserDapperRepository : DapperRepository<CompanyNameProjectNameDbContext>, IUserDapperRepository
    {
       
        public UserDapperRepository(IDbContextProvider<CompanyNameProjectNameDbContext> dbContextProvider) : base(dbContextProvider)
        {
           
        }

        public  async Task<List<string>> GetAllUserNameListAsync()
        {
            var connection = await GetDbConnectionAsync();
            return (await connection.QueryAsync<string>("select Name from AbpUsers")).ToList();
        }
    }
}
