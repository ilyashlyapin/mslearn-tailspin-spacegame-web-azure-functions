using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TailSpin.SpaceGame.Web
{
    public class LeaderboardFunctionClient : ILeaderboardServiceClient
    {
        private readonly string _functionUrl;
        private static readonly HttpClient _httpClient = new HttpClient();

        public LeaderboardFunctionClient(string functionUrl)
        {
            this._functionUrl = functionUrl;
        }

        async public Task<LeaderboardResponse> GetLeaderboard(int page, int pageSize, string mode, string region)
        {
            try
            {
                string url = $"{this._functionUrl}?page={page}&pageSize={pageSize}&mode={mode}&region={region}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LeaderboardResponse>(json);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exceptions
                // Log the exception or rethrow it
                throw new Exception("Error fetching leaderboard data", e);
            }
            catch (JsonException e)
            {
                // Handle JSON deserialization exceptions
                // Log the exception or rethrow it
                throw new Exception("Error deserializing leaderboard data", e);
            }
        }
    }
}