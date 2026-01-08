using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Modularity;
using Yitter.IdGenerator;

namespace Lion.AbpPro.IdGenerator;

public class AbpProIdGeneratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var logger = context.Services.BuildServiceProvider().GetRequiredService<ILogger<AbpProIdGeneratorModule>>();
        // 从环境变量或配置文件读取 WorkerId, 如果没有则随机生成从0-63 随机获取一个
        var workerId = Environment.GetEnvironmentVariable("WORKER_ID") ?? new Random().Next(0, 64).ToString();
        logger.LogInformation($"当前雪花算法WorkerId: {workerId}");
        var options = new IdGeneratorOptions
        {
            WorkerId = ushort.Parse(workerId)
        };
        YitIdHelper.SetIdGenerator(options);
    }
}