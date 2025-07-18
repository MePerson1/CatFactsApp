using CatFactsApp.Interfaces;
using CatFactsApp.Objects;

namespace CatFactsApp
{
    internal class CatFactsApp
    {
        private readonly ICatFactRepository _catFactRepository;
        private readonly IFileHandler _fileHandler;

        public CatFactsApp(ICatFactRepository catFactRepository, IFileHandler fileHandler)
        {
            _catFactRepository = catFactRepository;
            _fileHandler = fileHandler;
        }

        public void Run()
        {
            int count = 0;
            string path = "";
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            CatFact catFact = new CatFact();

            while (cki.Key != ConsoleKey.Escape)
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
                Console.WriteLine("To add more facts - press any key \nWant to exit app? - press ESC");
                cki = Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
