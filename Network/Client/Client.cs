﻿using System;
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

    public async Task StartAsync(string senderName, string ip)
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
        try
        {
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
            await SentMsgAsync(senderName, serverEndPoint); ;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        finally
        { 
            udpClient.Close(); 
        }
    }

    private async Task ReceiveConfirmationAsync(IPEndPoint serverEndPoint)
    {
        try
        {
            while (true)
            {
                byte[] confirmation = udpClient.Receive(ref serverEndPoint);
                string confirmationMsg = Encoding.UTF8.GetString(confirmation);
                Console.WriteLine("Server confirmation: " + confirmationMsg);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }

    private async Task SentMsgAsync(string senderName, IPEndPoint serverEndPoint)
    {
        try
        {
            while (true)
            {
                string msgText;
                do
                {
                    Console.WriteLine("Enter message text('Exit' to exit):");
                    msgText = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(msgText));
                Message message = new Message() { Text = msgText, Sender = senderName, Reciver = "Sevrer", MessageTime = DateTime.Now };

                string serialisedMsg = message.SerializeMessageToJson();
                byte[] data = Encoding.UTF8.GetBytes(serialisedMsg);

                Task sendTask =  udpClient.SendAsync(data, data.Length, serverEndPoint);
                await sendTask;

                if (msgText.ToLower().Equals("exit"))
                {
                    Console.WriteLine("Shutting down");   
                    break;
                }

                UdpReceiveResult receiveResult = await udpClient.ReceiveAsync();
                byte[] confirmation = receiveResult.Buffer;
                string confirmationMsg = Encoding.UTF8.GetString(confirmation);
                Console.WriteLine("Server confirmation: " + confirmationMsg);
 

            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
