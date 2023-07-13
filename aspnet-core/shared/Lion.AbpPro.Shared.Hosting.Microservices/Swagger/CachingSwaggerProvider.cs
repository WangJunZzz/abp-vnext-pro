namespace Swagger;

/// <summary>
/// swagger注释加载慢，把文档添加到缓存
/// </summary>
[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ISwaggerProvider))]
public class CachingSwaggerProvider : ISwaggerProvider, ITransientDependency
{
    private static readonly ConcurrentDictionary<string, OpenApiDocument> _cache = new ConcurrentDictionary<string, OpenApiDocument>();

    private readonly SwaggerGenerator _swaggerGenerator;

    public CachingSwaggerProvider(
        IOptions<SwaggerGeneratorOptions> optionsAccessor,
        IApiDescriptionGroupCollectionProvider apiDescriptionsProvider,
        ISchemaGenerator schemaGenerator)
    {
        _swaggerGenerator = new SwaggerGenerator(optionsAccessor.Value, apiDescriptionsProvider, schemaGenerator);
    }

    public OpenApiDocument GetSwagger(string documentName, string host = null, string basePath = null)
    {
        return _cache.GetOrAdd(documentName, (_) => _swaggerGenerator.GetSwagger(documentName, host, basePath));
    }
}