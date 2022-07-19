namespace Lion.AbpPro.Shared.Hosting.Microservices.Swaggers
{
    /// <summary>
    /// 在使用nswag的时候，原生默认的api导致生产的代理类存在问题
    /// 所有隐藏原生的api，重写路由
    /// </summary>
    public class HiddenAbpDefaultApiFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    string key = "/" + apiDescription.RelativePath;
                    var reuslt = IsHidden(key);
                    if (reuslt) swaggerDoc.Paths.Remove(key);
                }
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
}