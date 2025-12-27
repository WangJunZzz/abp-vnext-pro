using Lion.AbpPro.Ddd.Application.Contracts;

namespace Lion.AbpPro.Demo;

public interface IDemoAppService: IAbpProCrudAppService<DemoGetOutput,DemoGetListOutput,Guid,DemoGetListInput,DemoCreateInput,DemoUpdateInput>
{
    
}