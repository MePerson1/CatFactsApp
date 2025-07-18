using CatFactsApp.Objects;

namespace CatFactsApp.Interfaces
{
    internal interface ICatFactRepository
    {
        public Task<CatFact?> GetFact();
    }
}
