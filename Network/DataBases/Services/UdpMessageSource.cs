using DataBases.Arbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnitTests;

namespace DataBases.Services
{
    internal class UdpMessageSource : IMessageSource
    {
        private readonly UdpClient _udpClient;

        public UdpMessageSource()
        {
        }

        public UdpMessageSource(UdpClient udpClient)
        {
            _udpClient = new UdpClient();
        }

        public NetMessage Recieve(ref IPEndPoint endPoint)
        {
            byte[] data = _udpClient.Receive(ref endPoint);

            string str = Encoding.UTF8.GetString(data);
            return NetMessage.DeserializeMessgeFromJSON(str) ?? new NetMessage();
        }

        public async Task SendAsync(NetMessage message, IPEndPoint endPoint)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessageToJSON());

           await _udpClient.SendAsync(buffer, buffer.Length, endPoint);

        }
    }
}
