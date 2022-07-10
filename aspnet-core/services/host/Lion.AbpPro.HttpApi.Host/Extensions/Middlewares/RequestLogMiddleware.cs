namespace Lion.AbpPro.Extensions.Middlewares;

public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLogMiddleware> _logger;

    public RequestLogMiddleware(RequestDelegate next,
        ILogger<RequestLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var originalBody = context.Response.Body;
        if (context.Request.Path.ToString().ToLower().Contains("swagger")
            || context.Request.Path.ToString().ToLower().Contains("login")
            || context.Request.Path.ToString().ToLower().Contains("monitor")
            || context.Request.Path.ToString().ToLower().Contains("cap")
            || context.Request.Path.ToString().ToLower().Contains("hangfire")
            || context.Request.Path.ToString() == "/"
           )
        {
            await _next(context);
        }
        else
        {
            try
            {
                var logRequestId = Guid.NewGuid().ToString();
                await RequestDataLog(context, logRequestId);
                using (var ms = new MemoryStream())
                {
                    context.Response.Body = ms;
                    await _next(context);
                    ResponseDataLog(ms, logRequestId);
                    ms.Position = 0;
                    await ms.CopyToAsync(originalBody);
                }
            }
            catch (Exception ex)
            {
                // 记录异常                        
                _logger.LogError(ex.Message + "" + ex.InnerException);
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }

    private async Task RequestDataLog(HttpContext context,
        string requestId)
    {
        var request = context.Request;
        var body = new StreamReader(request.Body);
        var requestData = $" 请求路径:{request.Path}\r\n 请求Body参数:{await body.ReadToEndAsync()}";
        _logger.LogInformation($"日志中间件[Request],LogRequestId:{requestId}:请求接口信息:{requestData}");
        request.Body.Position = 0;
    }

    private void ResponseDataLog(MemoryStream ms, string requestId)
    {
        ms.Position = 0;
        var responseBody = new StreamReader(ms).ReadToEnd();
        // 去除 Html
        var isHtml = Regex.IsMatch(responseBody, "<[^>]+>");
        if (!isHtml && !string.IsNullOrEmpty(responseBody))
        {
            _logger.LogInformation($"日志中间件[Response],LogRequestId:{requestId}:响应接口信息:{responseBody}");
        }
    }
}