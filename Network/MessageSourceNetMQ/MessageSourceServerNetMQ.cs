using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatCommon.Abstractions;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;

namespace MessageSourceNetMQ
{
    public class MessageSourceServerNetMQ : IMessageSourceServer<NetMQSocket>
    {
        private ResponseSocket _serverSocket;

        public MessageSourceServerNetMQ(string serverAddress)
        {
            _serverSocket = new ResponseSocket();
            _serverSocket.Bind(serverAddress);
        }

        public NetMQSocket CopyEndpoint(IPEndPoint ep)
        {
            ResponseSocket socket = new();
            socket.Connect($"tcp://{ep.Address}:{ep.Port}");
            return socket;
        }

        public NetMQSocket CreateEndpoint()
        {
            return new ResponseSocket();
        }

        public NetMessage Receive(ref NetMQSocket ep)
        {
            var message = ep.ReceiveFrameString();
            return new NetMessage { Text = message }; ;
        }

        public async Task SendAsync(NetMessage message, NetMQSocket ep)
        {
            ep.SendFrame(message.Text);

            await Task.Delay(1000);
        }
    }
}
