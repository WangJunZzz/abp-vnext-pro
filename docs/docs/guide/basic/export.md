---
outline: deep
---

# 集成

### 在module中注册导出服务
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    ConfigureMagicodes(context);
}

/// <summary>
/// 配置Magicodes.IE 导入导出
/// </summary>
private void ConfigureMagicodes(ServiceConfigurationContext context)
{
    context.Services.AddTransient<IExporter, ExcelExporter>();
    context.Services.AddTransient<IExcelExporter, ExcelExporter>();
}
```

### 导出接口示例

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

### 前端导出事件
::: code-group
```ts [vben2.8]
export function exportAsync({ request }) {
  openFullLoading();
  const _userServiceProxy = new UsersServiceProxy();
  _userServiceProxy.export(request).then((res) => {
    const a = document.createElement('a');
    a.href = URL.createObjectURL(res.data);
    a.download = '用户列表导出.xlsx';
    a.click();
    closeFullLoading();
  });
}
```
```ts [vben5]
const exportData = async () => {
  gridApi.setLoading(true);
  try {
    const formValues = await gridApi.formApi.getValues();
    const {
      pager: { currentPage, pageSize },
    } = await gridApi.grid.getProxyInfo();
    const pagination = { pageIndex: currentPage, pageSize };
    const { data } = await fileRequest.post(
      '/Users/export',
      { ...formValues, ...pagination },
      { responseType: 'blob' },
    );
    const url = window.URL.createObjectURL(new Blob([data]));
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', '用户列表导出.xlsx');
    document.body.append(link);
    link.click();
    link.remove();
    window.URL.revokeObjectURL(url);
  } finally {
    gridApi.setLoading(false);
  }
};
```
:::