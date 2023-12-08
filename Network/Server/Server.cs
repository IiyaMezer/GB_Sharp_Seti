﻿using System;
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
    private bool ServerIsRunning = true;

    public void ServerStart()
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50000);
        udpServer = new UdpClient(serverEndPoint);
        Console.WriteLine("Waiting for message...");        
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    private void ReceiveMessages()
    {
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);

        try
        {
            while (ServerIsRunning)
            {
                byte[] buffer = udpServer.Receive(ref clientEndPoint);
                if (buffer == null) break;
                var messageText = Encoding.UTF8.GetString(buffer);

                Message newMessage = Message.DeserializeMessageToJson(messageText);
                newMessage.ResieveConfirmation();


                byte[] cofirm = Encoding.UTF8.GetBytes("Message resieved");
                udpServer.Send(cofirm, cofirm.Length, clientEndPoint);
                if (messageText.ToLower().Equals("exit"))
                {
                    Console.WriteLine("Press any key to shutdown server");
                    Console.ReadKey();                    
                    ServerIsRunning = false;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}") ;
        }
        finally
        {
            udpServer.Close();
            Console.WriteLine("Zavershenie raboti.");
        }

    }
}
