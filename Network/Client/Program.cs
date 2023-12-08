namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();
        var clientThread = new System.Threading.Thread(() => client.Start(args[0], args[1]));
        clientThread.Start();
        clientThread.Join();

    }
}