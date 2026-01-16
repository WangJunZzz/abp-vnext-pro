using Lion.AbpPro.Ddd.Application;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Lion.AbpPro.Demo;

//[Route("Demo")]
public class DemoAppService : AbpProCrudAppService<
    DemoAggregate,
    DemoGetOutput,
    DemoGetListOutput,
    Guid,
    DemoGetListInput,
    DemoCreateInput,
    DemoUpdateInput>, IDemoAppService
{
    public DemoAppService(IRepository<DemoAggregate, Guid> repository) : base(repository)
    {
    }
}