namespace CatFactsApp.Interfaces
{
    internal interface IFileHandler
    {
        public string GenerateFile(string filename);
        public void SaveDataToFile(string path, string content);
    }
}
