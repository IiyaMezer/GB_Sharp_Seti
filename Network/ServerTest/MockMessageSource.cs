using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataBases.Arbstracts;
using UnitTests;

namespace ServerTest
{
    internal class MockMessageSource : IMessageSource
    {
        public NetMessage Recieve(ref IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(NetMessage message, IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }
    }
}
