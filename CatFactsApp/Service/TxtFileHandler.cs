using CatFactsApp.Interfaces;

namespace CatFactsApp.Service
{
    internal class TxtFileHandler : IFileHandler
    {
        public void SaveDataToFile(string path, string content)
        {
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(content);
            }
        }

        public List<string> ReadFile(string path) => File.ReadAllLines(path).ToList<string>();
    }
}
