using System;
using System.Threading.Tasks;

namespace DH_Server
{
    public class Program { 
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Server has started!");
            DH_Server_App srvr = new DH_Server_App();

            await srvr.StartAsync(); // an asynchronous call

            srvr.Stop();

            Console.WriteLine("Server has finished!");
        }
    }
}
