using System.Text;
using Lion.AbpPro;
using Lion.AbpPro.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization.ExceptionHandling;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class AbpProExceptionFilter : IAsyncExceptionFilter, ITransientDependency
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

        var logger = context.GetService<ILogger<AbpProExceptionFilter>>(NullLogger<AbpProExceptionFilter>.Instance);

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