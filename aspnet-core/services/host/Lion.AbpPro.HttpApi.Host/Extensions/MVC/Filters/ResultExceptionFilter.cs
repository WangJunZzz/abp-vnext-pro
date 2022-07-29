namespace Lion.AbpPro.Extensions.MVC.Filters
{
    public sealed class ResultExceptionFilter : IAsyncExceptionFilter, ITransientDependency
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
            if (context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
            {
                return true;
            }

            if (context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
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

            var logger = context.GetService<ILogger<ResultExceptionFilter>>(NullLogger<ResultExceptionFilter>.Instance);

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
            var localizer = context.GetService<IStringLocalizer<AbpProResource>>();
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
                    if (context.Exception is IHasErrorCode codeException)
                    {
                        result.SetFail(localizer[codeException.Code]);
                        foreach (var key in context.Exception.Data.Keys)
                        {
                            result.SetFail(result.Message.Replace("{" + key + "}", context.Exception.Data[key]?.ToString()));
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
}