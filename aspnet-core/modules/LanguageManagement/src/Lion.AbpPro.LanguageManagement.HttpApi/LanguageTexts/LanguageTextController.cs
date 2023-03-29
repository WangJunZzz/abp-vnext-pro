namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

[Route("LanguageTexts")]
public class LanguageTextController : LanguageManagementController, ILanguageTextAppService
{
    private readonly ILanguageTextAppService _languageTextAppService;

    public LanguageTextController(ILanguageTextAppService languageTextAppService)
    {
        _languageTextAppService = languageTextAppService;
    }

    [HttpPost("AllResource")]
    [SwaggerOperation(summary: "获取所有资源", Tags = new[] { "LanguageTexts" })]
    public async Task<List<FromSelector<string, string>>> AllResourceListAsync()
    {
        return await _languageTextAppService.AllResourceListAsync();
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询语言文本", Tags = new[] { "LanguageTexts" })]
    public async Task<PagedResultDto<PageLanguageTextOutput>> PageAsync(PageLanguageTextInput input)
    {
        return await _languageTextAppService.PageAsync(input);
    }

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建语言文本", Tags = new[] { "LanguageTexts" })]
    public async Task CreateAsync(CreateLanguageTextInput input)
    {
        await _languageTextAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑语言文本", Tags = new[] { "LanguageTexts" })]
    public async Task UpdateAsync(UpdateLanguageTextInput input)
    {
        await _languageTextAppService.UpdateAsync(input);
    }
}