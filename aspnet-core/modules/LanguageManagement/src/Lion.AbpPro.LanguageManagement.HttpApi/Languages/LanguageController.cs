namespace Lion.AbpPro.LanguageManagement.Languages;

[Route("Languages")]
public class LanguageController : LanguageManagementController, ILanguageAppService
{
    private readonly ILanguageAppService _languageAppService;

    public LanguageController(ILanguageAppService languageAppService)
    {
        _languageAppService = languageAppService;
    }

    [HttpPost("All")]
    [SwaggerOperation(summary: "获取所有语言", Tags = new[] { "Languages" })]
    public async Task<List<PageLanguageOutput>> AllListAsync()
    {
        return await _languageAppService.AllListAsync();
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询语言", Tags = new[] { "Languages" })]
    public async Task<PagedResultDto<PageLanguageOutput>> PageAsync(PageLanguageInput input)
    {
        return await _languageAppService.PageAsync(input);
    }

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建语言", Tags = new[] { "Languages" })]
    public async Task CreateAsync(CreateLanguageInput input)
    {
        await _languageAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑语言", Tags = new[] { "Languages" })]
    public async Task UpdateAsync(UpdateLanguageInput input)
    {
        await _languageAppService.UpdateAsync(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除语言", Tags = new[] { "Languages" })]
    public async Task DeleteAsync(DeleteLanguageInput input)
    {
        await _languageAppService.DeleteAsync(input);
    }

    [HttpPost("SetDefault")]
    [SwaggerOperation(summary: "设置默认语言", Tags = new[] { "Languages" })]
    public async Task SetDefaultAsync(IdInput input)
    {
        await _languageAppService.SetDefaultAsync(input);
    }
}