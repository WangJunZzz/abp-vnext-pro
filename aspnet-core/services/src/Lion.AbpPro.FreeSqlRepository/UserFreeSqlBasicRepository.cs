using Lion.AbpPro.Users;
using Lion.AbpPro.Users.Dto;

namespace Lion.AbpPro.FreeSqlRepository
{
    public class UserFreeSqlBasicRepository : FreeSqlBasicRepository, IUserFreeSqlBasicRepository
    {
        public async Task<List<UserOutput>> GetListAsync()
        {
            var sql = "select id from AbpUsers";
            var result = await FreeSql.Select<UserOutput>()
            .WithSql(sql)
            .ToListAsync();
            return result;
        }
    }
}
