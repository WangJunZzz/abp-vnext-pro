using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.QueryManagement.Systems.Users;

namespace CompanyName.ProjectName.QueryManagement.FreeSqlMySql.Systems.Users
{
    public class UserFreeSqlRepository : FreeSqlBasicRepository, IUserFreeSqlRepository
    {
        /// <summary>
        /// 根据用户id获取用户名称
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AbpUserFreeSqlDto> GetUserNameByIdAsync(Guid id)
        {
     
            var sql = $"select a.Id,a.Name,a.UserName,a.Email from AbpUsers a where a.Id=?id";
            var se= await FreeSql.Select<AbpUserFreeSqlDto>()
                .WithSql(sql,new {id})
                .ToListAsync();
            return null;
        }
    }
}