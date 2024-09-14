using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Lion.AbpPro.HttpClient;

/// <summary>
/// Demo
/// </summary>
public interface IDemoHttpClient : IHttpApi
{
    /// <summary>
    /// 获取天气信息
    /// </summary>
    [HttpGet("api/WeatherForecast/Get")]
    Task<IEnumerable<WeatherForecast>> GetAsync(CancellationToken token = default);


    [HttpPost("api/WeatherForecast/Demo1")]
    Task<IEnumerable<WeatherForecast>> PostAsync([JsonContent] WeatherForecast weatherForecasts, CancellationToken token = default);
}