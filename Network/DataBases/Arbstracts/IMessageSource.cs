using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnitTests;
using DataBases.Models;


namespace DataBases.Arbstracts
{
    public interface IMessageSource
    {
        Task SendAsync(NetMessage message, IPEndPoint endPoint);

        NetMessage Recieve(ref IPEndPoint endPoint);
    }
}
