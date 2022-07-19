namespace Lion.AbpPro.Shared.Hosting.Microservices.Swaggers
{
    /// <summary>
    /// swagger 枚举映射，
    /// 原因：前端代理生成枚举是数字
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var array = new OpenApiArray();
                array.AddRange(Enum.GetNames(context.Type).Select(n => new OpenApiString(n)));
                // NSwag
                schema.Extensions.Add("x-enumNames", array);
                // Openapi-generator
                schema.Extensions.Add("x-enum-varnames", array);
            }
        }
    }
}