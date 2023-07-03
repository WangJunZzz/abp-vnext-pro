namespace Lion.AbpPro.Core;


public class CustomListResultDto<T> 
{
    public IReadOnlyList<T> Items
    {
        get { return _items ??= new List<T>(); }
        set => _items = value;
    }

    private IReadOnlyList<T> _items;

    public CustomListResultDto()
    {
    }

    public CustomListResultDto(IReadOnlyList<T> items)
    {
        Items = items;
    }
}