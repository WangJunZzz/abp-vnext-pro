using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzz.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Zzz.Extensions
{
    public static class RecurringJobsExtensions
    {
        public static void CreateRecurringJob(this IServiceProvider service)
        {
            //var job = service.GetService<TestJob>();
            //RecurringJob.AddOrUpdate("测试Job", () => job.ExecuteAsync(), CronType.Minute(1));
        }
    }
}
