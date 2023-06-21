using System.Diagnostics;
using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Lion.AbpPro.EntityFrameworkCore.Tests.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Services;

public class BlogAppService : ApplicationService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IRepository<Post, Guid> _postRepository;
    private readonly IRepository<Comment, Guid> _commentRepository;
    private readonly IdentityRoleManager _identityRoleManager;

    public BlogAppService(IBlogRepository blogRepository, IdentityRoleManager identityRoleManager, IRepository<Post, Guid> postRepository, IRepository<Comment, Guid> commentRepository)
    {
        _blogRepository = blogRepository;
        _identityRoleManager = identityRoleManager;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }


    /// <summary>
    /// 批量插入10000条数据
    /// </summary>
    public async Task CreateAsync(int qty = 10000)
    {
        // mock 数据
        var list = GenFu.GenFu.ListOf<Blog>(qty);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _blogRepository.InsertManyAsync(list);
        stopwatch.Stop();
        Logger.LogInformation($"批量插入{list.Count}条,耗时(单位:毫秒):{stopwatch.ElapsedMilliseconds}");
    }

    /// <summary>
    /// 批量插入10000条数据
    /// </summary>
    public async Task CreateAllAsync(int qty = 10000)
    {
        // mock 数据
        var blogs = GenFu.GenFu.ListOf<Blog>(qty);
        var posts = new List<Post>();
        var comments = new List<Comment>();
        // blog和post一对多,post和comment一对多
        // 有主外键关系,所以循环mock数据
        foreach (var blog in blogs)
        {
            posts.Add(new Post(GuidGenerator.Create(), blog.Id, "name"));
        }


        foreach (var post in posts)
        {
            comments.Add(new Comment(GuidGenerator.Create(), 1, post.Id, "content"));
        }
        
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        // 需要执行三次,不会因为ef有定义关系而一次性插入posts和comments
        await _blogRepository.InsertManyAsync(blogs);
        await _postRepository.InsertManyAsync(posts);
        await _commentRepository.InsertManyAsync(comments);
        stopwatch.Stop();
        Logger.LogInformation($"批量插入blogs:{blogs.Count},posts:{posts.Count},comments:{comments.Count}条,耗时(单位:毫秒):{stopwatch.ElapsedMilliseconds}");
    }

    /// <summary>
    /// 批量插入10000条数据,并且测试事务是否和其它业务逻辑保持一致
    /// 测试结果：在一个事务内
    /// </summary>
    public async Task CreateTransactionAsync(int qty = 10)
    {
        var list = GenFu.GenFu.ListOf<Blog>(qty);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _blogRepository.InsertManyAsync(list);
        stopwatch.Stop();
        Logger.LogInformation($"批量插入{list.Count}条,耗时(单位:毫秒):{stopwatch.ElapsedMilliseconds}");
        await _identityRoleManager.CreateAsync(new IdentityRole(GuidGenerator.Create(), GuidGenerator.Create().ToString()));
        throw new UserFriendlyException("test");
    }

    public async Task CreateManualAsync()
    {
        var list = new List<Blog>
        {
            new Blog()
            {
                Name = "001",
                //CreationTime = Clock.Now,
            }
        };

        await _blogRepository.InsertManyAsync(list);
    }


    /// <summary>
    /// 批量更新
    /// <see cref="https://learn.microsoft.com/zh-cn/ef/core/saving/execute-insert-update-delete"/>
    /// </summary>
    public async Task BatchUpdateAsync(int qty = 10000)
    {
        using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
        {
            var list = GenFu.GenFu.ListOf<Blog>(qty);
            await _blogRepository.InsertManyAsync(list);
            await uow.CompleteAsync();
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var dbSet = await _blogRepository.GetDbSetAsync();
        await dbSet.ExecuteUpdateAsync(setters => setters
            .SetProperty(x => x.IsDeleted, x => true)
            .SetProperty(x => x.Name, x => "test"));
        stopwatch.Stop();
        Logger.LogInformation($"批量更新{qty}条,耗时(单位:毫秒):{stopwatch.ElapsedMilliseconds}");
    }

    /// <summary>
    /// 批量删除
    /// <see cref="https://learn.microsoft.com/zh-cn/ef/core/saving/execute-insert-update-delete"/>
    /// </summary>
    public async Task BatchDeleteAsync(int qty = 10000)
    {
        using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
        {
            var list = GenFu.GenFu.ListOf<Blog>(qty);
            await _blogRepository.InsertManyAsync(list);
            await uow.CompleteAsync();
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var dbSet = await _blogRepository.GetDbSetAsync();
        await dbSet.ExecuteDeleteAsync();
        stopwatch.Stop();
        Logger.LogInformation($"批量删除{qty}条,耗时(单位:毫秒):{stopwatch.ElapsedMilliseconds}");
    }
}