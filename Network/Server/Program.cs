using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

internal class Program
{
    static void Main(string[] args)
    {
        Server server = new Server(12345);
        var serverThread = new System.Threading.Thread(server.Start);
        serverThread.Start();
        serverThread.Join();
    }
    
}