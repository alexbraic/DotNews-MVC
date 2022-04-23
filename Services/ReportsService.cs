using System.Net.Http;
using DotNews.Data;

namespace DotNews.Services
{
    public class ReportsService
    {
        public HttpClient Client { get; set; }
        public ReportsService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:7290/");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;

        }
        public async Task<IEnumerable<Report>> GetReportList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Report>>("/api/Reports");
        }
    }
}
