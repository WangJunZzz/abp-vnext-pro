using Volo.Abp.Dapper;

namespace Lion.AbpPro.EntityFrameworkCore.Mysql;

[DependsOn(typeof(AbpEntityFrameworkCoreMySQLModule))]
[DependsOn(typeof(AbpDapperModule))]
public class AbpProEntityFrameworkCoreMysqlModule : AbpModule
{
}