namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public interface ILionAbpProCapDbProviderInfoProvider
{
    LionAbpProCapDbProviderInfo GetOrNull(string dbProviderName);
}