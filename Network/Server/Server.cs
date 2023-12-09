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
    private UdpClient udpServer;
    private CancellationTokenSource cts;

    public Server(int port)
    {
        udpServer = new UdpClient(port);
        cts = new CancellationTokenSource();

    }

    public void Start()
    {
        Console.WriteLine("Server started.");
        Console.WriteLine("Waiting for messages...");
        

        try
        {
            while (true)
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = udpServer.Receive(ref clientEndPoint);

                if (buffer == null)
                    break;

                string messageText = Encoding.UTF8.GetString(buffer);

                Message newMessage = Message.DeserializeMessageToJson(messageText);
                newMessage.ReceiveConfirmation();

           
                byte[] confirmation = Encoding.UTF8.GetBytes("Message received");
                udpServer.Send(confirmation, confirmation.Length, clientEndPoint);
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
                newMessage.ReceiveConfirmation();


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
