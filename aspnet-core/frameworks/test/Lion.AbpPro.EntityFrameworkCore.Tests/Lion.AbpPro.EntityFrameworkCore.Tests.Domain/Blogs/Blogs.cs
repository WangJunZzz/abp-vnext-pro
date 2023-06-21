using Volo.Abp.Domain.Entities.Auditing;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;

/// <summary>
/// 博客
/// </summary>
public class Blog :  FullAuditedAggregateRoot<Guid>
{
        private Blog()
        {
            Posts = new List<Post>();
        }
 
               
        public Blog(
            Guid id,
            string name,
            string code
        ) : base(id)
        {
             SetName(name);
             SetCode(code);
             Posts = new List<Post>();
        }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string  Name { get; private set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; private set; }
    
        /// <summary>
        /// 文章  一对多
        /// </summary>
        public List<Post> Posts  { get; private set; }
                    
        
        /// <summary>
        /// 设置名称
        /// </summary>        
        private void SetName(string name)
        {
            Name = name;
        }     
        
        /// <summary>
        /// 设置编码
        /// </summary>        
        private void SetCode(string code)
        {
            Code = code;
        }     
      
        /// <summary>
        /// 更新博客
        /// </summary> 
        public void Update(
            string name,
            string code
        )
        {
             SetName(name);
             SetCode(code);
        }
}