namespace Lion.AbpPro.Cli;

public interface ICodeService
{
    Task<string> GetAccessTokenAsync();

    Task<string> DownloadAsync(string accessToken, Guid projectId, Guid templateId);
}