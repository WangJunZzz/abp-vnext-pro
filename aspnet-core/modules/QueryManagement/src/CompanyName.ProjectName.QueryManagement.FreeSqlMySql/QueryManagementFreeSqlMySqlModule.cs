using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace CompanyName.ProjectName.QueryManagement.FreeSqlMySql
{
    public class QueryManagementFreeSqlMySqlModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureVirtualFileSystem();
            
            var configuration = context.Services.GetConfiguration();
            var connectionString = configuration.GetConnectionString(QueryManagementDbProperties.ConnectionStringName);
            var freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                .UseMonitorCommand(cmd => Console.WriteLine($"线程：{cmd.CommandText}\r\n"))
                .UseNoneCommandParameter(true)
                .Build();
         
            context.Services.AddSingleton<IFreeSql>(freeSql);

        }
        
        private void ConfigureVirtualFileSystem()
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<QueryManagementFreeSqlMySqlModule>();
            });
        }
    }
}
