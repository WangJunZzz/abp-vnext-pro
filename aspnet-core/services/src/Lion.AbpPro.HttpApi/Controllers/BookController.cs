using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Lion.AbpPro.Books;

namespace Lion.AbpPro.Controllers;

[Route("Books")]
public class BookController : AbpController, IBookAppService
{
    private readonly IBookAppService _bookAppService;

    public BookController(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询书籍", Tags = new[] { "Books" })]     
    public async Task<PagedResultDto<PageBookOutput>> PageAsync(PageBookInput input)
    {
        return await _bookAppService.PageAsync(input);
    }  

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建书籍", Tags = new[] { "Books" })]        
    public async Task CreateAsync(CreateBookInput input)
    {
        await _bookAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑书籍", Tags = new[] { "Books" })]         
    public async Task UpdateAsync(UpdateBookInput input)
    {
        await _bookAppService.UpdateAsync(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除书籍", Tags = new[] { "Books" })]         
    public async Task DeleteAsync(DeleteBookInput input)
    {
        await _bookAppService.DeleteAsync(input);
    }     
}