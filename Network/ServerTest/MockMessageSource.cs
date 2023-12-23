using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataBases.Arbstracts;
using DataBases.Services;
using UnitTests;

namespace ServerTest
{
    internal class MockMessageSource : IMessageSource 
    {

        private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Any, 0);

        private Queue<NetMessage> _messages = new Queue<NetMessage>();

        private Server _server;

        public MockMessageSource()
        {
            _messages.Enqueue(new NetMessage { Command = Command.Register, Sender = "Ilya" });
            _messages.Enqueue(new NetMessage { Command = Command.Register, Sender = "Irina" });
            _messages.Enqueue(new NetMessage { Command = Command.Message, Sender = "Irina", Reciver = "Ilya", Text = "Irina's message" });
            _messages.Enqueue(new NetMessage { Command = Command.Message, Sender = "Ilya", Reciver = "Irina", Text = "Ilya's message" });
        }

        public NetMessage Recieve(ref IPEndPoint ep)
        {
            ep = _endPoint;

            if (true)
            {
                _server.Stop();

                return null;
            }

            return _messages.Dequeue();

        }

        public Task SendAsync(NetMessage message, IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public void AddServer(Server serv)
        {
            _server = serv;
        }
    }
}
