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
    public UdpClient udpClient;
    public Client()
    {
        udpClient = new UdpClient();
    }
    public void Start(string ip, string senderName)
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
        var sendThread = new Thread(() => SentMsg(senderName, serverEndPoint));
        var recieveThread = new Thread(() => RecieveConfirmation());
        sendThread.Start();
        recieveThread.Start();

        sendThread.Join();
        recieveThread.Join(); 
        udpClient.Close();

    }

    private void RecieveConfirmation()
    {
        try
        {
            while (true)
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] confirmation = udpClient.Receive(ref serverEndPoint);
                string confirmationMsg = Encoding.UTF8.GetString(confirmation);
                Console.WriteLine("Server confirmation: " + confirmationMsg);
            }
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private void SentMsg(string senderName, IPEndPoint serverEndPoint)
    {        

        while (true)
        {
            string msgText;
            do
            {
                //Console.Clear();
                Console.WriteLine("Enter message text:");
                msgText = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(msgText));
            Message message = new Message() { Text = msgText, Sender = senderName, Reciver = "Sevrer", MessageTime = DateTime.Now };
            string serialisedMsg = message.SerializeMessageToJson();
            byte[] data = Encoding.UTF8.GetBytes(serialisedMsg);
            udpClient.Send(data, data.Length, serverEndPoint);

            if (msgText.ToLower() == "exit")
            {
                Console.WriteLine("Client is shutting down.");
                break;
            }

        }

    }
}
