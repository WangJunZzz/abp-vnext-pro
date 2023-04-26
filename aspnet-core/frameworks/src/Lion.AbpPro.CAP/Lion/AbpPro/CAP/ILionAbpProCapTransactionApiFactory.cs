namespace Lion.AbpPro.CAP;

public interface ILionAbpProCapTransactionApiFactory
{
    Type TransactionApiType { get; }
    
    ITransactionApi Create(ITransactionApi originalApi);
}