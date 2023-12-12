using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server;

public class ConsoleMessageObserver : IMessageObserver
{
    public void NotifyMessageReceived(Message message)
    {
        message.ReceiveConfirmation();
    }
}
