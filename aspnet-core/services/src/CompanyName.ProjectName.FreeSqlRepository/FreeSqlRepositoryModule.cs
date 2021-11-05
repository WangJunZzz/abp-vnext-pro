using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.FreeSqlRepository
{
    public class FreeSqlRepositoryModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var connectionString =
                configuration.GetConnectionString("Default");
            var freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                .UseMonitorCommand(cmd => Console.WriteLine($"线程：{cmd.CommandText}\r\n"))
                .UseNoneCommandParameter(true)
                .Build();

            context.Services.AddSingleton<IFreeSql>(freeSql);
        }
    }
}