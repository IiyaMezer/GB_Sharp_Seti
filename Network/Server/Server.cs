using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Server;

public class Server
{
    private readonly ObserverManager _observerManager = new ObserverManager();
    private static Server _instance;//singleton
    private UdpClient udpServer;
    
    private CancellationTokenSource cts;

    public void SubcribeObserver(IMessageObserver observer)
    {
        _observerManager.Subscribe(observer);
    }

    public void UnsubcribeObserver(IMessageObserver observer)
    {
        _observerManager.Unsubscribe(observer);
    }


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

                Message newMessage = Message.DeserializeMessageToJson(messageText);
                _observerManager.NotifyMessageReceived(newMessage);


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
}
