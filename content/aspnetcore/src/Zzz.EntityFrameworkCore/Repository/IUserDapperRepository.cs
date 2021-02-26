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
    public interface IUserDapperRepository : ITransientDependency
    {

        Task<List<string>> GetAllUserNameListAsync();
    }
}
