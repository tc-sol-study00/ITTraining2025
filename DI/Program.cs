using DI.Properties;
using DI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DI {
    internal class Program {
        static void Main(string[] args) {

            var services = new ServiceCollection();
            services.AddTransient<Service_Layer1>();
            services.AddTransient<Service_Layer2>();
            services.AddTransient<Service_Layer3>();
            services.AddTransient<Common>();
        }
    }
}
