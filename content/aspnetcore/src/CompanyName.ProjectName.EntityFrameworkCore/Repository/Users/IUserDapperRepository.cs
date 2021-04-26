using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CompanyNameProjectName.Repository
{
    public interface IUserDapperRepository : ITransientDependency
    {

        Task<List<string>> GetAllUserNameListAsync();
    }
}
