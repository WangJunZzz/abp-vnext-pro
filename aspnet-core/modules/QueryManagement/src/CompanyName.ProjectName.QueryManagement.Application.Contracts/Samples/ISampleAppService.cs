using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.QueryManagement.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
