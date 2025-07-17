using CatFactsApp.Objects;
using System.Text.Json;

namespace CatFactsApp
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var maxLoops = 10;
            string path = "";
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            CatFact catFact;

            while (cki.Key != ConsoleKey.Escape && maxLoops > 0)
            {
                catFact = await GetFactsFromApi();
                if (path == "")
                    path = GenerateFile();

                if (catFact != null && path != "")
                {
                    Console.WriteLine(new string('.', Console.WindowWidth - 1));
                    Console.WriteLine("CAT FACT");
                    Console.WriteLine(catFact.Fact);
                    Console.WriteLine(new string('.', Console.WindowWidth - 1));
                    SaveToFile(path, catFact);
                }

                Console.WriteLine(new string('-', Console.WindowWidth - 1));
                Console.WriteLine("For more facts - any key \nWant to exit app? - ESC");
                maxLoops--;
                cki = Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task<CatFact> GetFactsFromApi()
        {
            var client = new HttpClient();
            var endpoint = new Uri("https://catfact.ninja/fact");
            CatFact catFact = new CatFact();

            var result = await client.GetAsync(endpoint);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                try
                {
                    var convertedJsonCatFact = JsonSerializer.Deserialize<CatFact>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (convertedJsonCatFact != null)
                    {
                        catFact = convertedJsonCatFact;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Json deserialize error: {ex.Message}");
                }

            }
            else
            {
                Console.WriteLine($"Error: {result.StatusCode}");
            }

            return catFact;
        }

        static string GenerateFile()
        {
            var filename = $"catfacts_{DateTime.UtcNow:dd-MM-yyyy_HH-mm-ss}.txt";
            var path = Path.Combine(Environment.CurrentDirectory, filename);

            using (FileStream fs = File.Create(path)) { }

            return path;
        }

        static void SaveToFile(string path, CatFact catFact)
        {
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {

                writer.WriteLine($"- {catFact.Fact} ({catFact.Length})");

            }
            Console.WriteLine($"\nSaved cat fact to:\n{path}\n");
        }
    }
}
