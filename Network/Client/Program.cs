namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();
        client.Start(args[0],args[1]);
    }
}