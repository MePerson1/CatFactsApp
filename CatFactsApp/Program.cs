namespace CatFactsApp
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("https://catfact.ninja/fact");
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        Console.WriteLine(json);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {result.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }
    }
}
