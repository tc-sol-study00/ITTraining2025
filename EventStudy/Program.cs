using Convenience.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStudy {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

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
            serviceCollection.AddTransient<Form1>();
            serviceCollection.AddTransient<Chumon>();

            // Build provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve and run
            var form1 = serviceProvider.GetRequiredService<Form1>();

            //ApplicationConfiguration.Initialize();
            Application.Run(form1);
        }
    }
}