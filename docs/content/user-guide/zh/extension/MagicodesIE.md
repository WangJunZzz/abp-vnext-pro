## 配置

```csharp
/// <summary>
/// 配置Magicodes.IE 导入导出
/// </summary>
private void ConfigureMagicodes(ServiceConfigurationContext context)
{
    context.Services.AddTransient<IExporter, ExcelExporter>();
    context.Services.AddTransient<IExcelExporter, ExcelExporter>();
}
```

## 示例

```csharp
/// <summary>
/// 用户导出列表
/// </summary>
/// <returns></returns>
[Authorize(AbpProPermissions.SystemManagement.UserExport)]
public async Task<ActionResult> ExportAsync(PagingUserListInput input)
{
    var request = new GetIdentityUsersInput
    {
        Filter = input.Filter?.Trim(),
        MaxResultCount = input.PageSize,
        SkipCount = input.SkipCount,
        Sorting = " LastModificationTime desc"
    };
    List<Volo.Abp.Identity.IdentityUser> source = await _identityUserRepository
        .GetListAsync(request.Sorting, request.MaxResultCount, request.SkipCount, request.Filter);
    var result = ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<ExportIdentityUserOutput>>(source);
    var bytes = await _excelExporter.ExportAsByteArray<ExportIdentityUserOutput>(result);
    return new XlsxFileResult(bytes: bytes, fileDownloadName: $"用户导出列表{DateTime.Now:yyyyMMdd}");
}

```