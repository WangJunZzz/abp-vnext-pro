namespace Lion.AbpPro.Shared.Hosting.Microservices.Volo.Abp
{
    /// <summary>
    /// 修改Abp 返回状态码
    /// 原因： 抛出BusinessException异常 不应该抛403异常
    /// </summary>
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