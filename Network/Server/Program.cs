using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        Server server = Server.GetInstance(12345);
        //server.Start();
        Task serverTask = server.StartAsync();
         var observerA = new ConsoleMessageObserver();
        var observerB = new ConsoleMessageObserver();

        server.SubcribeObserver(observerA);
        server.SubcribeObserver(observerB);
        server.UnsubcribeObserver(observerA);

        Console.WriteLine("Press Enter to stop the server.");
        Console.ReadLine();
        server.Stop();
        await serverTask;
    }
    
}