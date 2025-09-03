using System.Threading.Tasks;

namespace Hidoki {
    internal class Program {
        static async Task Main(string[] args) {
            Hidoki hidoki = new Hidoki();
            Task<int> task = hidoki.HidokiMethodAsync();

            int ttl = 0;
            for (int i = 0; i < 10000; i++) ttl += i;

            int result = await task;

            ttl += result;

            Console.WriteLine(ttl);
        }


    }

    public class Hidoki {
        public async Task<int> HidokiMethodAsync() {
            await Task.Delay(30000);    //30s

            return 1;
        }
    }
}
