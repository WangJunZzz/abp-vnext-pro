using System.ComponentModel;

namespace Lion.AbpPro.Books;

/// <summary>
/// 类型
/// </summary>
public enum BookType
{
    [Description("历史")] History = 10,
    [Description("小说")] Story = 20,
    [Description("其它")] Other = 30,
}