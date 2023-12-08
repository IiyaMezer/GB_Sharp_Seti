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

    public Server(int port)
    {
        udpServer = new UdpClient(port);
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

                // Блок с ДЗ
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
}
