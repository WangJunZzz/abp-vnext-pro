using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;

namespace Microsoft.AspNetCore.Mvc.Filters;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(AbpExceptionFilter))]
public class AbpProExceptionFilter : AbpExceptionFilter
{
    protected override bool ShouldHandleException(ExceptionContext context)
    {
        return IsWrapResult(context) || base.ShouldHandleException(context);
    }

    protected override async Task HandleAndWrapException(ExceptionContext context)
    {
        if (!IsWrapResult(context))
        {
            await base.HandleAndWrapException(context);
            return;
        }

        LoggerException(context);
        WrapResultHandler(context);
        context.ExceptionHandled = true;
    }

    private void LoggerException(ExceptionContext context)
    {
        var logger = context.GetService<ILogger<AbpExceptionFilter>>(NullLogger<AbpExceptionFilter>.Instance)!;
        var logLevel = context.Exception.GetLogLevel();
        logger.LogException(context.Exception, logLevel);
    }

    /// <summary>
    /// webapi有WrapResult特性标签处理逻辑
    /// </summary>
    private void WrapResultHandler(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = 200;
        var result = SimplifyMessage(context);
        context.Result = new ObjectResult(result);
    }

    private bool IsWrapResult(ExceptionContext context)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            return false;
        }

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

    private WrapResult<object> SimplifyMessage(ExceptionContext context)
    {
        var result = new WrapResult<object>();
        var localizer = context.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
        switch (context.Exception)
        {
            case AbpAuthorizationException:
                result.SetFail(localizer["Lion.AbpPro:PermissionDenied"], "401");
                break;
            case AbpValidationException validationException:
                var errorMessage = localizer["Lion.AbpPro:ParameterValidationFailed"] + ";" + validationException.ValidationErrors.JoinAsString(";");
                result.SetFail(errorMessage, "400");
                break;
            case EntityNotFoundException:
                result.SetFail(localizer["Lion.AbpPro:EntityNotFound"], "506");
                break;
            case NotImplementedException:
                result.SetFail(localizer["Lion.AbpPro:Unimplemented"], "507");
                break;
            case DbUpdateConcurrencyException:
                result.SetFail(localizer["Lion.AbpPro:DbUpdateConcurrency"], "508");
                break;
            default:
            {
                if (context.Exception is IHasErrorCode codeException)
                {
                    var exceptionConverter = context.GetRequiredService<IAbpProExceptionConverter>();
                    var message = exceptionConverter.TryToLocalizeExceptionMessage(context.Exception);
                    if (codeException.Code.IsNullOrWhiteSpace())
                    {
                        // TODO 没有code，不应该出现这个情况。
                        result.SetFail(context.Exception.Message);
                    }
                    else
                    {
                        result.SetFail(message, codeException.Code);
                    }
                }
                else
                {
                    result.SetFail(context.Exception.Message);
                }

                break;
            }
        }

        return result;
    }
}
