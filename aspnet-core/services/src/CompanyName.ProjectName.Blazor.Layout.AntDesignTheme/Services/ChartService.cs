using CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Services
{
    public class ChartService : IChartService
    {
        private readonly HttpClient _httpClient;

        public ChartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChartDataItem[]> GetVisitDataAsync()
        {
            return (await GetChartDataAsync()).VisitData;
        }

        public async Task<ChartDataItem[]> GetVisitData2Async()
        {
            return (await GetChartDataAsync()).VisitData2;
        }

        public async Task<ChartDataItem[]> GetSalesDataAsync()
        {
            return (await GetChartDataAsync()).SalesData;
        }

        public async Task<RadarDataItem[]> GetRadarDataAsync()
        {
            return (await GetChartDataAsync()).RadarData;
        }

        private async Task<ChartData> GetChartDataAsync()
        {
            return await _httpClient.GetFromJsonAsync<ChartData>("data/fake_chart_data.json");
        }
    }
}
