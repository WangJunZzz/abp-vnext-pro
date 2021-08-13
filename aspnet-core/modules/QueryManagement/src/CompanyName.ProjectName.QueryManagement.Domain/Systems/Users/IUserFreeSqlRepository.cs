using System;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.QueryManagement.Systems.Users
{
    public interface IUserFreeSqlRepository
    {
        Task<AbpUserFreeSqlDto> GetUserNameByIdAsync(Guid id);
    }
}
