using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;

/// <summary>
/// 评论
/// </summary>
public class Comment : FullAuditedEntity<Guid>
{
    public Comment()
    {
    }


    public Comment(
        Guid id,
        int? star,
        Guid postId,
        string content
    ) : base(id)
    {
        SetStar(star);
        SetPostId(postId);
        SetContent(content);
    }

    /// <summary>
    /// 点赞
    /// </summary>
    public int? Star { get; set; }

    /// <summary>
    /// 外键
    /// </summary>
    public Guid PostId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }


    /// <summary>
    /// 设置点赞
    /// </summary>        
    private void SetStar(int? star)
    {
        Star = star;
    }

    /// <summary>
    /// 设置外键
    /// </summary>        
    private void SetPostId(Guid postId)
    {
        PostId = postId;
    }

    /// <summary>
    /// 设置内容
    /// </summary>        
    private void SetContent(string content)
    {
        Content = content;
    }
}