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
    public async void ServerMsg(string name)
    {
        UdpClient server = new UdpClient(12345);
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.WriteLine("Server started.");
        Console.WriteLine("Waiting for message...");

        while (!cts.IsCancellationRequested)
        {
            byte[] buffer = server.Receive(ref clientEndPoint);
            if (buffer == null) break;
            var messageText = Encoding.UTF8.GetString(buffer);

            Message newMessage = Message.DeserializeMessageToJson(messageText);
            newMessage.ResieveConfirmation();
            
            byte[] cofirm = Encoding.UTF8.GetBytes("Message resieved");
            server.Send(cofirm, cofirm.Length, clientEndPoint);
            if (newMessage.Text.ToLower().Equals("exit"))
            {
                newMessage.PrintText();
                await Console.Out.WriteLineAsync("press any key to shutdown server");
                Console.ReadLine();
                cts.Cancel();
            }

            
        }

    }
}
