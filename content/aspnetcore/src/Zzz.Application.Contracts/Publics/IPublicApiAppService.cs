using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Zzz.Publics
{
    public interface IPublicApiAppService : IApplicationService
    {
        Task<string> TestAsync(string msg);
    }
}
