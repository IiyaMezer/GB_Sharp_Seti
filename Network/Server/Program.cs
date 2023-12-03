using System.Net;
using System.Net.Sockets;
namespace Server;

internal class Program
{
    static void Main(string[] args)
    {
        Message msg = new Message() { Text="Hi", MessageTime = DateTime.Now, RecievedFrom ="Sender", RecievedTo="Reciever" };
        string serialisedMsg = msg.SerializeMessageToJson();
        Console.WriteLine(serialisedMsg);
        Message deserialisedMsg = Message.DeserializeMessageToJson(serialisedMsg);
    }
    //public bool SendMessage(string message)
    //{
    //    using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
    //    {
    //        var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
    //        listener.Blocking = true;
    //        listener.Bind(remoteEndpoint);
    //        listener.Listen(100);

    //        Console.WriteLine("wait");
    //        var socket = listener.Accept();

    //        Console.WriteLine("Connected");
    //        listener.Close();
    //    }
    //}
}