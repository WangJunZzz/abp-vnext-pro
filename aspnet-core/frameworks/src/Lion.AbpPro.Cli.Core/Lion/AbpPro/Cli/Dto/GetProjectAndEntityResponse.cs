namespace Lion.AbpPro.Cli.Dto;

public class GetProjectAndEntityResponse
{
    public GetProjectAndEntityResponse()
    {
        Entities = new List<EntityOutput>();
    }

    public ProjectOutput Project { get; set; }

    public List<EntityOutput> Entities { get; set; }
}

public class ProjectOutput
{
    public Guid Id { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }
}

public class EntityOutput
{
    public Guid Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }


    /// <summary>
    /// 首字母小写
    /// </summary>
    public string CodeCamelCase { get; set; }

    /// <summary>
    /// 复数形式
    /// </summary>
    public string CodePluralized { get; set; }
}