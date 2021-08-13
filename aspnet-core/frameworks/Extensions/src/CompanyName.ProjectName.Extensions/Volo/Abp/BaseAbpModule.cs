using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace CompanyName.ProjectName.Extensions.Volo.Abp
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpJsonModule))]
    [DependsOn(typeof(AbpValidationModule))]
    [DependsOn(typeof(AbpDddDomainModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpObjectMappingModule))]
    public class BaseAbpModule:AbpModule
    {
        
    }
}