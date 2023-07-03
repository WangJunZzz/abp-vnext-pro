namespace Lion.AbpPro.CAP.EntityFrameworkCore;

public interface IAbpProCapDbProviderInfoProvider
{
    AbpProCapDbProviderInfo GetOrNull(string dbProviderName);
}