using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Zzz.Publics
{
    [RemoteService]
   public class PublicApiAppService : ApplicationService, IPublicApiAppService
    {
        public async Task<string> TestAsync(string msg)
        {
            await Task.CompletedTask;
            return msg;
        }
    }
}
