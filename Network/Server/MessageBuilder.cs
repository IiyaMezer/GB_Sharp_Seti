using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server;

public class MessageBuilder
{
    private readonly Message _message;

    public MessageBuilder()
    {
        _message = new Message();
    }
    public MessageBuilder Text(string text)
    {
        _message.Text = text;
        return this;
    }
    public MessageBuilder From(string senderName)
    {
        _message.Sender = senderName;
        return this;
    }
    public MessageBuilder To(string reciever)
    {
        _message.Reciver = reciever;
        return this;
    }
    public MessageBuilder Time(DateTime time)
    {
        _message.MessageTime = time;
        return this;
    }

    public Message Built()
    {
        return _message;
    }
}
