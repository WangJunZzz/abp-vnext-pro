using Microsoft.OpenApi;

namespace Swagger;

/// <summary>
/// 在使用nswag的时候，原生默认的api导致生产的代理类存在问题
/// 所有隐藏原生的api，重写路由
/// </summary>
public class HiddenAbpDefaultApiFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // 首先收集需要移除的路径
        var pathsToRemove = new List<string>();
        
        foreach (ApiDescription apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.TryGetMethodInfo(out MethodInfo method))
            {
                string key = "/" + apiDescription.RelativePath;
                var result = IsHidden(key);
                if (result) 
                    pathsToRemove.Add(key);
            }
        }

        // 移除匹配的路径
        foreach (var path in pathsToRemove)
        {
            swaggerDoc.Paths.Remove(path);
        }

        // 检查并移除空的标签（分组）
        RemoveEmptyTags(swaggerDoc);
    }

    private void RemoveEmptyTags(OpenApiDocument swaggerDoc)
    {
        if (swaggerDoc.Tags == null || !swaggerDoc.Tags.Any())
            return;

        // 获取所有路径中使用的标签
        var usedTags = new HashSet<string>();
        foreach (var pathItem in swaggerDoc.Paths)
        {
            foreach (var operation in pathItem.Value.Operations.Values)
            {
                if (operation.Tags != null)
                {
                    foreach (var tag in operation.Tags)
                    {
                        usedTags.Add(tag.Name);
                    }
                }
            }
        }

        // 移除未使用的标签
        var tagsToRemove = swaggerDoc.Tags.Where(tag => !usedTags.Contains(tag.Name)).ToList();
        foreach (var tag in tagsToRemove)
        {
            swaggerDoc.Tags.Remove(tag);
        }
    }

    private bool IsHidden(string key)
    {
        var list = GetHiddenAbpDefaultApiList();
        foreach (var item in list)
        {
            if (key.Contains(item)) return true;
        }

        return false;
    }

    private List<string> GetHiddenAbpDefaultApiList()
    {
        return new List<string>() {
            "/api/abp/multi-tenancy/tenants",
            "/api/account",
            "/api/feature-management/features",
            "/api/permission-management/permissions",
            "/api/identity/my-profile",
            "/api/identity",
            "/api/multi-tenancy/tenants",
            "/api/setting-management/emailing",
            "/configuration",
            "/outputcache"
        };
    }
}