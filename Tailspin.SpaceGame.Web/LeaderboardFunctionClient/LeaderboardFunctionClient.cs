using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TailSpin.SpaceGame.Web
{
    public class LeaderboardFunctionClient : ILeaderboardServiceClient
    {
        private string _functionUrl;

        public LeaderboardFunctionClient(string functionUrl)
        {
            this._functionUrl = functionUrl;
        }

        async public Task<LeaderboardResponse> GetLeaderboard(int page, int pageSize, string mode, string region)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = $"{this._functionUrl}?page={page}&pageSize={pageSize}&mode={mode}&region={region}";
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<LeaderboardResponse>(json);
                }
            }
    }
}
