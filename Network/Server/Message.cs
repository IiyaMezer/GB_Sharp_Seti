using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server;

public class Message
{
    public string Text { get; set; }
    public string Sender { get; set; }
    public string Reciver { get; set; }
    public DateTime MessageTime { get; set; }

    public string SerializeMessageToJson() => JsonSerializer.Serialize(this);

    public static Message? DeserializeMessageToJson(string jsonMessage) => JsonSerializer.Deserialize<Message>(jsonMessage);

    public void ResieveConfirmation()
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
