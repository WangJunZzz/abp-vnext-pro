namespace Lion.AbpPro;

public interface IAbpProExceptionConverter
{
    string TryToLocalizeExceptionMessage(Exception exception);
}