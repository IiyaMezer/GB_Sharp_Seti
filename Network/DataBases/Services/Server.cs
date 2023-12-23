using DataBases.Arbstracts;
using DataBases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnitTests;

namespace DataBases.Services;

public class Server
{


    Dictionary<string, IPEndPoint> _clients = new Dictionary<string, IPEndPoint>();
    private IPEndPoint endPoint;
    private readonly IMessageSource _messageSource;
    public Server()
    {
        _messageSource = new UdpMessageSource();

    }

    public async Task Start()
    {

        endPoint = new IPEndPoint(IPAddress.Any, 0);
        UdpClient udpClient = new UdpClient(12345);

        Console.WriteLine("Server waiting for messageses");

        while (true)
        {
            try
            {
                var message = _messageSource.Recieve(ref endPoint);
                message.ReceiveConfirmation();

                await ProcessMessage(message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex); ;
            }
        }
    }


    private async Task ProcessMessage(NetMessage message)
    {
        switch (message.Command)
        {
            case Command.Register: await Register(message); break;
            case Command.Message: await RelyMessage(message); break;
            case Command.Confirmation: await ConfirmMessageReceivedAsync(message.Id); break;
            default:
                break;
        }
    }

    private async Task RelyMessage(NetMessage message)
    {
        if (_clients.TryGetValue(message.Reciver, out IPEndPoint endPoint))
        {
            int?  id= 0;
            using( var ctx = new ChatContext())
            {
                var fromUser = ctx.Users.First(x => x.Fullname == message.Sender);
                var toUser = ctx.Users.First(x => x.Fullname == message.Reciver);
                var msg = new Message { SenderId = fromUser, RecieverId = toUser, IsSent = false, Text = message.Text };
                ctx.Messages.Add(msg);
                
                ctx.SaveChangesAsync();

                id = msg.MessageId;
            }

            message.Id = id;

          await _messageSource.SendAsync(message, endPoint);

            Console.WriteLine($"Message Rlied, from = {message.Sender} to = {message.Reciver}");
        }

        else
        {
            Console.WriteLine($"User not found.");
        }
    }

   private async Task Register(NetMessage message)
    {
        Console.WriteLine($"Message Register name  = {message.Sender}");
        if (_clients.TryAdd(message.Sender, message.EndPoint))
        {
            using(ChatContext context = new ChatContext())
            {
                context.Users.Add(new User() { Fullname = message.Sender });
                await context.SaveChangesAsync();
            }
        }
    }

    async Task ConfirmMessageReceivedAsync(int? id)
    {
        Console.WriteLine("Msg confimation id = " + id);
        using( var ctx = new  ChatContext())
        {
            var msg = ctx.Messages.FirstOrDefault(x => x.MessageId == id);
            if (msg != null) 
            {
                msg.IsSent = true;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
