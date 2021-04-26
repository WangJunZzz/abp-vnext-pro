using Hangfire;
using Hangfire.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Hangfire;
using Volo.Abp.Modularity;
using CompanyNameProjectName.Options;

namespace CompanyNameProjectName
{
    [DependsOn(
        typeof(CompanyNameProjectNameApplicationModule),
        typeof(CompanyNameProjectNameDomainTestModule)
        )]
    public class CompanyNameProjectNameApplicationTestModule : AbpModule
    {
  
    }
}