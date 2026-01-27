using System.Text.Json.Nodes;
using Microsoft.OpenApi;

namespace Swagger;

/// <summary>
/// swagger 枚举映射，
/// 原因：前端代理生成枚举是数字
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is OpenApiSchema openApiScheme && context.Type.IsEnum)
        {
            openApiScheme.Enum?.Clear();
            openApiScheme.Type = JsonSchemaType.String;
            openApiScheme.Format = null;
            foreach (var name in Enum.GetNames(context.Type))
            {
                openApiScheme.Enum?.Add(JsonNode.Parse($"\"{name}\"")!);
            }
        }
    }
}