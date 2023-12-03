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
    public string RecievedFrom { get; set; }
    public string RecievedTo { get; set; }
    public DateTime MessageTime { get; set; }

    public string SerializeMessageToJson() => JsonSerializer.Serialize(this);

    public static Message? DeserializeMessageToJson(string jsonMessage) => JsonSerializer.Deserialize<Message>(jsonMessage);




}
