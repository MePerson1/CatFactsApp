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

        public CatFact? GetFact()
        {
            var result = _httpClient.GetStringAsync("https://catfact.ninja/fact").Result;

            return JsonSerializer.Deserialize<CatFact>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
}
