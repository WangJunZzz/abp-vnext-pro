namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public class AbpProCapDbProviderInfo
{
    public Type CapTransactionType { get; }
    
    public Type CapEfDbTransactionType { get; }
    
    public AbpProCapDbProviderInfo(string capTransactionTypeName, string capEfDbTransactionTypeName)
    {
        CapTransactionType = Type.GetType(capTransactionTypeName, false);
        CapEfDbTransactionType = Type.GetType(capEfDbTransactionTypeName, false);
    }
}