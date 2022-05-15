using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lion.AbpPro.Extension.Customs.Dtos;
using Lion.AbpPro.Extensions.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using Volo.Abp.Validation;

namespace Lion.AbpPro.Shared.Hosting.Microservices.Microsoft.AspNetCore.MVC.Filters
{
    public sealed class ResultExceptionFilter : IFilterMetadata, IAsyncExceptionFilter, ITransientDependency
    {
        private ILogger<ResultExceptionFilter> Logger { get; set; }

        private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AbpExceptionHandlingOptions _exceptionHandlingOptions;

        public ResultExceptionFilter(
            IExceptionToErrorInfoConverter errorInfoConverter,
            IHttpExceptionStatusCodeFinder statusCodeFinder,
            IJsonSerializer jsonSerializer,
            IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions)
        {
            _errorInfoConverter = errorInfoConverter;
            _statusCodeFinder = statusCodeFinder;
            _jsonSerializer = jsonSerializer;
            _exceptionHandlingOptions = exceptionHandlingOptions.Value;
            Logger = NullLogger<ResultExceptionFilter>.Instance;
        }

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
            //TODO: Trigger an AbpExceptionHandled event or something like that.

            context.HttpContext.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
            var statusCode = (int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception);
            context.HttpContext.Response.StatusCode = 200;

            var remoteServiceErrorInfo = _errorInfoConverter.Convert(context.Exception, options => { options.SendExceptionsDetailsToClients = _exceptionHandlingOptions.SendExceptionsDetailsToClients; });
            remoteServiceErrorInfo.Code = context.HttpContext.TraceIdentifier;
            remoteServiceErrorInfo.Message = SimplifyMessage(context.Exception);
            var result = new WrapResult<object>();
            result.SetFail(remoteServiceErrorInfo.Message);

            // HttpResponse
            context.Result = new ObjectResult(result);

            // 写日志
            var logLevel = context.Exception.GetLogLevel();
            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(_jsonSerializer.Serialize(remoteServiceErrorInfo, indented: true));
            Logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
            Logger.LogException(context.Exception, logLevel);

            await context.HttpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier>()
                .NotifyAsync(
                    new ExceptionNotificationContext(context.Exception)
                );

            context.Exception = null; //Handled!
        }

        private string SimplifyMessage(Exception error)
        {
            switch (error)
            {
                case AbpAuthorizationException e:
                    return "Authenticate failure！";
                case AbpValidationException e:
                    return "Request param validate failure！";
                case EntityNotFoundException e:
                    return "not found the entity！";
                case BusinessException e:
                    return $"{e.Message}";
                case NotImplementedException e:
                    return "not implement！";
                default:
                    return "server internal error！";
            }
        }
    }

}
