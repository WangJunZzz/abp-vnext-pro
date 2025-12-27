using Lion.AbpPro.Demo;
using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.Controllers;

public class DemoController : AbpProController,IDemoAppService
{
    private readonly IDemoAppService _demoAppService;

    public DemoController(IDemoAppService demoAppService)
    {
        _demoAppService = demoAppService;
    }

    public async Task<DemoGetOutput> GetAsync(Guid id)
    {
        return await _demoAppService.GetAsync(id);
    }

    public async Task<PagedResultDto<DemoGetListOutput>> GetListAsync(DemoGetListInput input)
    {
        return await _demoAppService.GetListAsync(input);
    }

    public async Task<DemoGetOutput> CreateAsync(DemoCreateInput input)
    {
        return await _demoAppService.CreateAsync(input);
    }

    public async Task<DemoGetOutput> UpdateAsync(Guid id, DemoUpdateInput input)
    {
        return await _demoAppService.UpdateAsync(id,input);
    }

    public async Task DeleteAsync(Guid id)
    {
         await _demoAppService.DeleteAsync(id);
    }

    public async Task DeleteAsync(IEnumerable<Guid> ids)
    {
        await _demoAppService.DeleteAsync(ids);
    }
}