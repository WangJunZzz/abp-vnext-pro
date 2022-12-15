namespace Lion.AbpPro.Core;

[Serializable]
public class CustomPagedResultDto<T> : CustomListResultDto<T>
{
    public long TotalCount { get; set; }

    public CustomPagedResultDto()
    {
    }

    public CustomPagedResultDto(long totalCount, IReadOnlyList<T> items)
        : base(items)
    {
        TotalCount = totalCount;
    }
}