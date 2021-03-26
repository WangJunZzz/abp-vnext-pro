using Hangfire;
using Hangfire.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Hangfire;
using Volo.Abp.Modularity;
using Zzz.Options;

namespace Zzz
{
    [DependsOn(
        typeof(ZzzApplicationModule),
        typeof(ZzzDomainTestModule)
        )]
    public class ZzzApplicationTestModule : AbpModule
    {
  
    }
}