using Castle.Components.DictionaryAdapter.Xml;
using DataBases.Arbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using UnitTests;

namespace DataBases.Services
{
    public class Client
    {
        private readonly string _name;
        string address;

        private readonly IMessageSource _messageSource;
        private IPEndPoint remoteEndPoint;
        UdpClient udpClient = new();

        public Client(string name, string address)
        {
            this._name = name;

            _messageSource = new UdpMessageSource();
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(address), 12345);
        }

        public async Task Start()
        {
            //udpClient = new UdpClient(port);

             await ClientListenerAsync();

            await ClientSenderAsync();
        }

        private async Task ClientListenerAsync()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(address), 12345);

            while (true)
            {
                try
                {
                    var messageRecieved = _messageSource.Recieve(ref endPoint);

                    Console.WriteLine($" Reciever message from {messageRecieved.Sender}:");
                    Console.WriteLine(messageRecieved.Text );

                    await ConfirmAsync(messageRecieved, endPoint);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }
        }

        private async Task ConfirmAsync(NetMessage messageRecieved, IPEndPoint endPoint)
        {
            messageRecieved.Command = Command.Confirmation;

           await _messageSource.SendAsync(messageRecieved, endPoint);
        }

       async Task ClientSenderAsync()
        {
            
            Register(remoteEndPoint);

            while (true)
            {
                try
                {
                    Console.Write("Enter Reciever Name & press Enter");
                    var nameTo = Console.ReadLine();

                    Console.Write("Enter message & p[ress Enter:");

                    var messageText = Console.ReadLine();

                    var message = new NetMessage() { Command = Command.Message, Sender = _name, Reciver = nameTo, Text = messageText };

                    await _messageSource.SendAsync(message, remoteEndPoint);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }
        }

        async Task Register(IPEndPoint endPoint)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var message = new NetMessage() { Sender = _name, Reciver = null, Text = null, Command = Command.Register, EndPoint = ep }; 
            await _messageSource.SendAsync(message, endPoint);
        }


    }
}
