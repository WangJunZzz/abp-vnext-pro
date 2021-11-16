using CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Models;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Services
{
    public interface IChartService
    {
        Task<ChartDataItem[]> GetVisitDataAsync();
        Task<ChartDataItem[]> GetVisitData2Async();
        Task<ChartDataItem[]> GetSalesDataAsync();
        Task<RadarDataItem[]> GetRadarDataAsync();
    }
}
