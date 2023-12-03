namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();
        Client.SentMsg(args[0],args[1]);
    }
}