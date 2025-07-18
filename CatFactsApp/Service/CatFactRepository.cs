using CatFactsApp.Interfaces;
using CatFactsApp.Objects;
using System.Text.Json;

namespace CatFactsApp.Service
{
    internal class CatFactRepository : ICatFactRepository
    {
        private readonly HttpClient _httpClient;

        public CatFactRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CatFact?> GetFact()
        {
            var result = await _httpClient.GetStringAsync("https://catfact.ninja/fact");

            return JsonSerializer.Deserialize<CatFact>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
}
