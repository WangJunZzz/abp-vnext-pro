namespace Lion.AbpPro.CAP;

public interface IAbpProCapTransactionApiFactory
{
    Type TransactionApiType { get; }
    
    ITransactionApi Create(ITransactionApi originalApi);
}