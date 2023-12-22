using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace DataBases;

public class Server
{
    private static Server _instance;//singleton
    private UdpClient udpServer;
    
    private CancellationTokenSource cts;

    private Server(int port)
    {
        udpServer = new UdpClient(port);
        
        cts = new CancellationTokenSource();

    }

    public static Server GetInstance(int port)
    {
        return _instance ??= new Server(port);
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Server started.");
        Console.WriteLine("Waiting for messages...");
        
        CancellationToken cancellationToken = cts.Token;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UdpReceiveResult receiveResult = await udpServer.ReceiveAsync();

                string messageText = Encoding.UTF8.GetString(receiveResult.Buffer);

                NetMessage newMessage = NetMessage.DeserializeMessageToJson(messageText);
                


                byte[] confirmation = Encoding.UTF8.GetBytes("Message received");
                await udpServer.SendAsync(confirmation, confirmation.Length, receiveResult.RemoteEndPoint);
                if (newMessage.Text.ToLower().Equals("exit"))
                {
                    Console.WriteLine("Server is shutting down. Press any key to exit.");
                    Console.ReadKey();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            udpServer.Close();
        }
    }
    public void Stop()
    {
        cts.Cancel();
    }

    void Register(NetMessage message, IPEndPoint endPoint)
    {
        Console.WriteLine("Message register, name = " + message.Sender);
        clients.Add(message.Sender, endPoint);
    }
}
