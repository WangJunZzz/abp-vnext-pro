using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Entities.Blogs;

/// <summary>
/// 博客
/// </summary>
public class Blog : FullAuditedAggregateRoot<Guid>
{
    public Blog()
    {
        Posts = new List<Post>();
    }
    public Blog(Guid id) : base(id)
    {
        Posts = new List<Post>();
    }

    public decimal Price { get; set; }

    public float PriceOne { get; set; }

    public bool IsShow { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public BlogType BlogType { get; set; }

    public List<Post> Posts { get; set; }
}