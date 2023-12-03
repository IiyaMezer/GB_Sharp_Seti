using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Server;

namespace Client;

public class Client
{
    public static void SentMsg(string senderName, string ip)
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

        while (true)
        {
            string msgText;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter message text:");
                msgText = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(msgText));
            Message message = new Message() { Text = msgText, Sender = senderName, Reciver = "Sevrer", MessageTime = DateTime.Now };
            string serialisedMsg = message.SerializeMessageToJson();
            byte[] data = Encoding.UTF8.GetBytes(serialisedMsg);

            udpClient.Send(data, data.Length, iPEndPoint);

            
        }

    }
}
