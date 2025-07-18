using CatFactsApp.Interfaces;
using CatFactsApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsApp
{
    internal class Program
    {
        //extra - zapis do XML (na początku wybór)
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddHttpClient();
            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddSingleton<ICatFactRepository, CatFactRepository>();
            services.AddTransient<CatFactsApp>();

            var provider = services.BuildServiceProvider();

            var app = provider.GetRequiredService<CatFactsApp>();

            app.Run();

        }
    }
}
