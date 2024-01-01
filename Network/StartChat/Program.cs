using ChatApp;
using System.Net;
using NetMQ;
using MessageSourceNetMQ;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Server<NetMQSocket> server = new(new MessageSourceServerNetMQ("tcp://127.0.0.1:5555"));
            await server.Start();
        }
        else
        if (args.Length == 1)
        {
            Client<NetMQSocket> client = new(new MessageSourceClientNetMQ(), args[0]);
            await client.Start();
        }
        else
        {

            Console.WriteLine("Для запуска сервера введите ник-нейм как параметр запуска приложения");
            Console.WriteLine("Для запуска клиента введите ник-нейм и IP сервера как параметры запуска приложения");
        }

        Console.ReadKey(true);
    }
}