using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IProjectNameDbContext : IEfCoreDbContext
    {

    }
}
