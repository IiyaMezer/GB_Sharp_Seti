namespace Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        Client client = new Client();
        //client.Start(args[0],args[1]);
        await client.StartAsync(args[0], args[1]);
    }
}
