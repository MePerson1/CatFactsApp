using CatFactsApp.Interfaces;
using CatFactsApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddHttpClient<ICatFactRepository, CatFactRepository>();
            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddTransient<CatFactsApp>();

            var provider = services.BuildServiceProvider();

            var app = provider.GetRequiredService<CatFactsApp>();

            await app.RunAsync();

        }
    }
}
