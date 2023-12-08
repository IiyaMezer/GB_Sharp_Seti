using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Server server = new Server();
            var serverThread = new Thread(server.ServerStart);
            serverThread.Start();
            serverThread.Join();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    
}