using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Validation;

 namespace CompanyNameProjectName.Extensions.Filters
{
    [Dependency(ReplaceServices = true)]
    public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder, ITransientDependency
    {
        protected AbpExceptionHttpStatusCodeOptions Options { get; }

        public DefaultHttpExceptionStatusCodeFinder(
            IOptions<AbpExceptionHttpStatusCodeOptions> options)
        {
            Options = options.Value;
        }

        public HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
        {
            if (exception is IHasHttpStatusCode exceptionWithHttpStatusCode &&
               exceptionWithHttpStatusCode.HttpStatusCode > 0)
            {
                return (HttpStatusCode)exceptionWithHttpStatusCode.HttpStatusCode;
            }

            if (exception is IHasErrorCode exceptionWithErrorCode &&
                !exceptionWithErrorCode.Code.IsNullOrWhiteSpace())
            {
                if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue(exceptionWithErrorCode.Code, out var status))
                {
                    return status;
                }
            }

            if (exception is AbpAuthorizationException)
            {
                return HttpStatusCode.Forbidden;
            }

            //TODO: Handle SecurityException..?

            if (exception is AbpValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            if (exception is NotImplementedException)
            {
                return HttpStatusCode.NotImplemented;
            }

            if (exception is IBusinessException)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
