using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzz.Jobs
{
    public class TestJob : IRecurringJob
    {
        public async Task ExecuteAsync()
        {
            await Task.CompletedTask;
            Log.Information("TestJob");
        }
    }
}
