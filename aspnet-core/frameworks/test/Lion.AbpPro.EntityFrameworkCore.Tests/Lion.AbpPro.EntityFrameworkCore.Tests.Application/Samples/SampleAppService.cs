using System.Threading.Tasks;
using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Microsoft.AspNetCore.Authorization;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Samples;

public class SampleAppService : TestsAppService, ISampleAppService
{
    private readonly IBlogRepository _blogRepository;

    public SampleAppService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public Task<SampleDto> GetAsync()
    {
        var blog = new Blog(GuidGenerator.Create(),GuidGenerator.Create().ToString(),"001");
        _blogRepository.InsertManyAsync(new List<Blog>() { blog });
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }
}
