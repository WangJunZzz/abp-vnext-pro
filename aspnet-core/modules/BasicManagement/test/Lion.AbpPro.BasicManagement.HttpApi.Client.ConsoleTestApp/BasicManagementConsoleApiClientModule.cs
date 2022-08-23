using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BasicManagementHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BasicManagementConsoleApiClientModule : AbpModule
{

}
