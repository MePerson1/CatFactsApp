﻿using CatFactsApp.Interfaces;
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

        public async Task RunAsync()
        {
            int count = 0;
            string path = "";
            ConsoleKeyInfo cki;
            CatFact? catFact = null;

            do
            {
                try
                {
                    var newCatFact = await _catFactRepository.GetFact();
                    if (newCatFact != null)
                    {
                        catFact = newCatFact;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] Something went wrong with API Connection: " + ex.Message);
                }

                if (path == "")
                    path = $"catfacts_{DateTime.UtcNow:dd-MM-yyyy_HH-mm-ss}.txt";

                if (catFact is not null && path != "")
                {
                    SaveFactToFile(path, $"{++count}. {catFact.Fact} - length({catFact.Length})");

                    Console.WriteLine($"Data saved in:\n{path}\n");
                    Console.WriteLine(new string('.', Console.WindowWidth - 1));
                    Console.WriteLine("\n CAT FACT \n");

                    DisplayFileContent(path);

                    Console.WriteLine(new string('.', Console.WindowWidth - 1));

                }


                Console.WriteLine("To add more facts to file - press any key \nWant to exit app? - press ESC");

                cki = Console.ReadKey();
                Console.Clear();

            } while (cki.Key != ConsoleKey.Escape);

            Console.WriteLine("->You can find the generated file in:\n" + Environment.CurrentDirectory + $"\\{path}");

        }

        private void SaveFactToFile(string path, string fact)
        {
            try
            {
                _fileHandler.SaveDataToFile(path, fact);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Something went wrong creating file: " + ex.Message);
            }
        }
        private void DisplayFileContent(string path)
        {
            try
            {
                foreach (string fact in _fileHandler.ReadFile(path))
                    Console.WriteLine(fact + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Something went wrong with reading file: " + ex.Message);
            }
        }

    }
}
