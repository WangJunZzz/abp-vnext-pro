using Lion.AbpPro.Ddd.Application.Contracts;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Ddd.Application;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpProDddApplicationContractsModule)
)]
public class AbpProDddApplicationModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        // 设置默认每页显示记录数
        LimitedResultRequestDto.DefaultMaxResultCount = 10;
            
        // 设置最大允许的每页记录数
        LimitedResultRequestDto.MaxMaxResultCount = 10000;
        
    }
}