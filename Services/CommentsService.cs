using DotNews.Data;

namespace DotNews.Services
{
    public class CommentsService
    {
        public HttpClient Client { get; set; }
        public CommentsService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:7209/");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;

        }
        public async Task<IEnumerable<Comment>> GetCommentList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>("/api/Comments");
        }

        public async Task<IEnumerable<Comment>> GetReport(int id)
        {
            return (IEnumerable<Comment>)await Client.GetFromJsonAsync<Comment>($"/api/Comments/{id}");
        }
    }
}
