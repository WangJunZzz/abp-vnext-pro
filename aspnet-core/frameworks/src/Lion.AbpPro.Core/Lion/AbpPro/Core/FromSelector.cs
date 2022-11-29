namespace Lion.AbpPro.Core;

public abstract class FromSelectorBase
{
    protected FromSelectorBase(int value, string label)
    {
        Value = value;
        Label = label;
    }

    public int Value { get; protected set; }
    public string Label { get; protected set; }
}

public abstract class FromSelectorBase<TValue, TLabel>
{
    protected FromSelectorBase(TValue value, TLabel label)
    {
        Value = value;
        Label = label;
    }

    public TValue Value { get; protected set; }
    public TLabel Label { get; protected set; }
}

public class FromSelector : FromSelectorBase
{
    public FromSelector(int value, string label) : base(value, label)
    {
    }
}

public class FromSelector<TValue, TLabel> : FromSelectorBase<TValue, TLabel>
{
    public FromSelector(TValue value, TLabel label) : base(value, label)
    {
    }
}