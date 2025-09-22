
using Convenience.Models.Properties;

namespace ChumonDebug {
    internal class Program {
        static async Task Main(string[] args) {
            Chumon chumon = new Chumon();



            var x=await chumon.ChumonToiawase("A000000001", new DateOnly(2025, 9, 12));
        }
    }
}
