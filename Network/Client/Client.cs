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
    private UdpClient udpClient;

    public Client() { udpClient = new UdpClient();}

    public void Start(string senderName, string ip)
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
        var sendThread = new Thread(() => SentMsg(senderName, serverEndPoint));
        var receiveThread = new Thread(() => ReceiveConfirmation(serverEndPoint));
        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
        sendThread.Start();
        receiveThread.Start();

        sendThread.Join();
        receiveThread.Join();
        udpClient.Close();

    }

    private void ReceiveConfirmation(IPEndPoint serverEndPoint)
    {
        while (true)
        {
            byte[] confirmation = udpClient.Receive(ref serverEndPoint);
            string confirmationMsg = Encoding.UTF8.GetString(confirmation);
            Console.WriteLine("Server confirmation: " + confirmationMsg);
        }

    }

    public void SentMsg(string senderName, IPEndPoint serverEndPoint)
    {
        try
        {
            while (true)
            {
                string msgText;
                do
                {
                    //Console.Clear();
                    Console.WriteLine("Enter message text('Exit' to exit):");
                    msgText = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(msgText));
                Message message = new Message() { Text = msgText, Sender = senderName, Reciver = "Sevrer", MessageTime = DateTime.Now };
                string serialisedMsg = message.SerializeMessageToJson();
                byte[] data = Encoding.UTF8.GetBytes(serialisedMsg);
                udpClient.Send(data, data.Length, serverEndPoint);
                if (msgText.ToLower().Equals("exit"))
                {
                    Console.WriteLine("Shutting down");
                    break;
                }
            }
        }catch (Exception ex) { Console.WriteLine(ex.Message); }


    }
}
