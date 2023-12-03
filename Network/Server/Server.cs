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
    public void ServerMsg(string name)
    {
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("Waiting for message...");

        while (true)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);
            if (buffer == null) break;
            var messageText = Encoding.UTF8.GetString(buffer);

            Message newMessage = Message.DeserializeMessageToJson(messageText);
            newMessage.Print();

        }

    }
}
