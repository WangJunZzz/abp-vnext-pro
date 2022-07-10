using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Lion.AbpPro.Extensions
{
     public static class SerilogToEsExtensions
    {
        public static void SetSerilogConfiguration(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        {
            // 默认读取 configuration 中 "Serilog" 节点下的配置
            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext();

            var writeToElasticSearch = configuration.GetValue("ElasticSearch:Enabled", false);


            // LogToElasticSearch:Enabled = true 才输出至ES
            if (!writeToElasticSearch)
                return;

            var applicationName = "Lion.AbpPro.HttpApi.Host";

            var esUrl = configuration["ElasticSearch:Url"];
            // 需要设置ES URL
            if (string.IsNullOrEmpty(esUrl))
                return;


            var indexFormat = configuration["ElasticSearch:IndexFormat"];

            // 需要设置ES URL
            if (string.IsNullOrEmpty(indexFormat))
                return;

            var esUserName = configuration["ElasticSearch:UserName"];
            var esPassword = configuration["ElasticSearch:Password"];

            loggerConfiguration.Enrich.FromLogContext().Enrich.WithExceptionDetails().WriteTo
                .Elasticsearch(BuildElasticSearchSinkOptions(esUrl, indexFormat, esUserName, esPassword));
            loggerConfiguration.Enrich.WithProperty("Application", applicationName);
        }

        // 创建Es连接
        private static ElasticsearchSinkOptions BuildElasticSearchSinkOptions(
            string url,
            string indexFormat,
            string userName,
            string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new ElasticsearchSinkOptions(new Uri(url))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = indexFormat
                };
            }

            return new ElasticsearchSinkOptions(new Uri(url))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = indexFormat,
                ModifyConnectionSettings = x => x.BasicAuthentication(userName, password)
            };
        }

        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            // 为每个请求都设置通用的属性
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
            // 如果要记录 Request Body 或 Response Body
            // 参考 https://stackoverflow.com/questions/60076922/serilog-logging-web-api-methods-adding-context-properties-inside-middleware
            string requestBody = ReadRequestBody(httpContext.Request).Result;
            if (!string.IsNullOrEmpty(requestBody))
            {
                diagnosticContext.Set("RequestBody", requestBody);
            }

            // string responseBody = ReadResponseBody(httpContext.Response).Result;
            // if (!string.IsNullOrEmpty(responseBody))
            // {
            //     diagnosticContext.Set("ResponseBody", requestBody);
            // }

            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{requestBody}";
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{responseBody}";
        }
    }
}