namespace CsharpStudy20250925 {
    using Convenience.Data;
    using Convenience.Models.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    namespace CsharpStudy20250925 {
        internal class Program {
            static void Main(string[] args) {
                // Build configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // DI container setup
                var serviceCollection = new ServiceCollection();

                serviceCollection.AddDbContext<ConvenienceContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("ConvenienceContext")
                        ?? throw new InvalidOperationException("Connection string 'ConvenienceContext' not found.")));

                // Register your service
                serviceCollection.AddTransient<DictionalyStudy>();
                serviceCollection.AddTransient<ChumonJissekiEnumerator>();
                serviceCollection.AddTransient<Iterator>();

                // Build provider
                var serviceProvider = serviceCollection.BuildServiceProvider();

                // Resolve and run
                var app = serviceProvider.GetRequiredService<DictionalyStudy>();
                app.Run();

                //
                var chumonM = serviceProvider.GetRequiredService<ChumonJissekiEnumerator>();

                foreach(var item in chumonM) {
                    chumonM.DisplayData(item);
                }

                var iterator = serviceProvider.GetRequiredService<Iterator>();

                foreach (var item in iterator) {
                    Console.WriteLine(item);
                }


            }
        }
    }

}
