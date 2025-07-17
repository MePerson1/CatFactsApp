using CatFactsApp.Objects;

namespace CatFactsApp.Interfaces
{
    internal interface ICatFactsApiService
    {
        public CatFact GetCatFact();
        public List<CatFact> GetCatFacts();
    }
}
