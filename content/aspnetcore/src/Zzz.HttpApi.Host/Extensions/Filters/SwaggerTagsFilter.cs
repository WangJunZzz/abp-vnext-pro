using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zzz.Extensions.Filters
{
    /// <summary>
    /// 把abp vnext 提供的api 归档
    /// </summary>
    public class SwaggerTagsFilter : IOperationFilter
    {
        public const string DefaultTagName = "ABP Vnext 默认 Api";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var tag = GetChineseTag(context.ApiDescription.ActionDescriptor as ControllerActionDescriptor);
            if (null != tag)
                operation.Tags = new List<OpenApiTag> { tag };
        }

        private static OpenApiTag GetChineseTag(ControllerActionDescriptor description)
        {
            if (null != description?.ControllerTypeInfo?.Namespace)
            {
                if (description.ControllerTypeInfo.Namespace.StartsWith("Volo.Abp") || description.ControllerTypeInfo.Namespace.StartsWith("Pages.Abp.MultiTenancy"))
                    return new OpenApiTag { Name = DefaultTagName };
                return null;
            }
            return null;
        }
    }
}
