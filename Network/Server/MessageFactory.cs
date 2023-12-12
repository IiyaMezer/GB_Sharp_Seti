using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server;

internal class MessageFactory : IMessageFactory
{
    public Message CreateMessage(string textInMsg, string nameFrom, string nameTo)
    {
        return new Message { Text = textInMsg, Sender = nameFrom, Reciver = nameTo, MessageTime = DateTime.Now };
    }
}

