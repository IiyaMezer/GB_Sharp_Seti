using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatCommon.Abstractions;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MessageSourceNetMQ
{
    public class MessageSourceClientNetMQ : IMessageSourceClient<NetMQSocket>
    {
        private RequestSocket _clientsocket;

        public MessageSourceClientNetMQ()
        {
            _clientsocket = new RequestSocket();
        }

        public NetMQSocket CreateEndpoint()
        {
            return new RequestSocket();
        }

        public NetMQSocket GetServer()
        {
            return new RequestSocket();
        }

        public NetMessage Receive(ref NetMQSocket ep)
        {
            byte[] data = ep.ReceiveFrameBytes();
            string str = Encoding.UTF8.GetString(data);
            return NetMessage.DeserializeMessgeFromJSON(str) ?? new NetMessage();
        }

        public async Task SendAsync(NetMessage message, NetMQSocket ep)
        {
            ep.SendFrame(message.Text);

            await Task.Delay(1000);
        }
    }
}
