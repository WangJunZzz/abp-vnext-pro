using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;

/// <summary>
/// 文章
/// </summary>
public class Post : FullAuditedEntity<Guid>
{
    public Post()
    {
        Comments = new List<Comment>();
    }


    public Post(
        Guid id,
        Guid blogId,
        string name
    ) : base(id)
    {
        SetBlogId(blogId);
        SetName(name);
        Comments = new List<Comment>();
    }

    /// <summary>
    /// 外键
    /// </summary>
    public Guid BlogId { get;  set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get;  set; }

    /// <summary>
    /// 评论  一对多
    /// </summary>
    public List<Comment> Comments { get;  set; }


    /// <summary>
    /// 设置外键
    /// </summary>        
    private void SetBlogId(Guid blogId)
    {
        BlogId = blogId;
    }

    /// <summary>
    /// 设置名称
    /// </summary>        
    private void SetName(string name)
    {
        Name = name;
    }
}