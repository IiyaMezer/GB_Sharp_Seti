namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Client client = new Client();
            var clientThread = new Thread(() => client.ClientStart(args[0], args[1]));
            //var clientThread = new Thread(() => client.ClientStart("120.0.0.1", "Ilya"));
            clientThread.Start();
            clientThread.Join();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
}