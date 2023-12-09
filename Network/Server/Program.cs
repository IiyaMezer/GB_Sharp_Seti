using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        Server server = new Server(12345);
        //server.Start();
        Task serverTask = server.StartAsync();
        Console.WriteLine("Press Enter to stop the server.");
        Console.ReadLine();
        server.Stop();
        await serverTask;
    }
    
}