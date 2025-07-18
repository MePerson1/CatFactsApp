namespace CatFactsApp.Interfaces
{
    internal interface IFileHandler
    {
        public void SaveDataToFile(string path, string content);
        public List<string> ReadFile(string path);
    }
}
