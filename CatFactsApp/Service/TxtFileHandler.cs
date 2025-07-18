using CatFactsApp.Interfaces;

namespace CatFactsApp.Service
{
    internal class TxtFileHandler : IFileHandler
    {
        // dont need this
        public string GenerateFile(string filename)
        {

            var path = Path.Combine(Environment.CurrentDirectory, filename + ".txt");

            using (FileStream fs = File.Create(path)) { }

            return path;
        }

        // function automaticly create file if not exists so i don't need GenerateFile
        public void SaveDataToFile(string path, string content)
        {
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(content);
            }
            Console.WriteLine($"Data saved in:\n{path}\n");
        }
    }
}
