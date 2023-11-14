using MongoDB.Entities;
using SearchService.Database;

namespace SearchService.Helpers
{
    public class AnimalServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AnimalServiceHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Animal>> GetAnimalsForSearchDb()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _httpClient.GetFromJsonAsync<List<Animal>>(_config["AnimalServiceUrl"]
                + "/api/animals");
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
