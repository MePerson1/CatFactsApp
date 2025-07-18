using CatFactsApp.Objects;
using CatFactsApp.Service;

namespace CatFactsApp
{
    internal class Program
    {
        //extra - zapis do XML (na początku wybór)
        //MVC? - serviceCollection?
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            CatFactRepository _catFactRepository = new CatFactRepository(client);
            TxtFileHandler _fileHandler = new TxtFileHandler();

            var maxLoops = 10;
            var count = 0;
            string path = "";
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            CatFact catFact = new CatFact();

            while (cki.Key != ConsoleKey.Escape && maxLoops > 0)
            {
                try
                {
                    var newCatFact = _catFactRepository.GetFact();
                    if (newCatFact != null)
                    {
                        catFact = newCatFact;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                if (path == "")
                    path = $"catfacts_{DateTime.UtcNow:dd-MM-yyyy_HH-mm-ss}.txt";

                if (catFact != null && path != "")
                {
                    Console.WriteLine(new string('.', Console.WindowWidth - 1));
                    Console.WriteLine("CAT FACT " + ++count);
                    Console.WriteLine(catFact.Fact);
                    Console.WriteLine(new string('.', Console.WindowWidth - 1));
                    _fileHandler.SaveDataToFile(path, $"{count}. {catFact.Fact} - lengt({catFact.Length})");
                }

                Console.WriteLine(new string('-', Console.WindowWidth - 1));
                Console.WriteLine("For more facts - press any key \nWant to exit app? - press ESC");
                maxLoops--;
                cki = Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
