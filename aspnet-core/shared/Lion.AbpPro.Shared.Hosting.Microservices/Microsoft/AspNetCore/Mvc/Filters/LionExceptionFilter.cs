using System.Text;

namespace Microservices.Microsoft.AspNetCore.Mvc.Filters;

public sealed class LionExceptionFilter : IAsyncExceptionFilter, ITransientDependency
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        if (!ShouldHandleException(context))
        {
            return;
        }

        await HandleAndWrapException(context);
    }

    private bool ShouldHandleException(ExceptionContext context)
    {
        if (context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.GetCustomAttributes(typeof(WrapResultAttribute), true).Any())
        {
            return true;
        }

        if (context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(typeof(WrapResultAttribute), true).Any())
        {
            return true;
        }

        return false;
    }

    private async Task HandleAndWrapException(ExceptionContext context)
    {
        var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
        });

        var logLevel = context.Exception.GetLogLevel();

        var remoteServiceErrorInfoBuilder = new StringBuilder();
        remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
        remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

        var logger = context.GetService<ILogger<LionExceptionFilter>>(NullLogger<LionExceptionFilter>.Instance);

        logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

        logger.LogException(context.Exception, logLevel);

        await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));
        context.HttpContext.Response.StatusCode = 200;
        var result = SimplifyMessage(context);
        context.Result = new ObjectResult(result);
        context.Exception = null; //Handled!
    }

    private WrapResult<object> SimplifyMessage(ExceptionContext context)
    {
        var result = new WrapResult<object>();
        switch (context.Exception)
        {
            case AbpAuthorizationException:
                result.SetFail("权限不足", 401);
                break;
            case AbpValidationException:
                result.SetFail("请求参数验证失败", 400);
                break;
            case EntityNotFoundException:
                result.SetFail("实体不存在", 506);
                break;
            case NotImplementedException:
                result.SetFail("未实现功能", 507);
                break;
            default:
            {
                result.SetFail(context.Exception.Message);
                break;
            }
        }

        return result;
    }
}