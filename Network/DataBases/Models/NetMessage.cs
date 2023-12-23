using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitTests
{
    public enum Command
    {
        Register,
        Message,
        Confirmation
    }

    public class NetMessage
    {

        public int? Id { get; set; }
        public string Text { get; set; }
        public DateTime MessageTime { get; set; }
        public string? Sender { get; set; }
        public string? Reciver { get; set; }
        public Command Command { get; set; }

        public IPEndPoint? EndPoint { get; set; }

        public string SerialazeMessageToJSON() => JsonSerializer.Serialize(this);

        public static NetMessage? DeserializeMessgeFromJSON(string message) => JsonSerializer.Deserialize<NetMessage>(message);

        public void PrintGetMessageFrom()
        {
            Console.WriteLine(ToString());
        }

        public void ReceiveConfirmation()
        {
            Console.WriteLine($"|{this.Reciver}| resieved msg: |{this.Text}| from |{this.Sender}| in time: |{this.MessageTime}|");
        }
        public override string ToString()
        {
            return
                $"Message data:\n" +
                $"Text: {Text}.\n" +
                $"Sender: {Sender}.\n" +
                $"Reciever: {Reciver}.\n" +
                $"Time: {MessageTime}.";
        }
    }
}