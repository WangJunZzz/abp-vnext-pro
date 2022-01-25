using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.FileManagement.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}