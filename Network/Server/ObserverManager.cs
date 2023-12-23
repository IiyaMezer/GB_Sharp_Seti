using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server;

public class ObserverManager: IMessageObserver
{
    private readonly List<IMessageObserver> _observers = new List<IMessageObserver>();

    public void NotifyMessageReceived(Message message)
    {
        message.ReceiveConfirmation();
    }

    public void Subscribe(IMessageObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IMessageObserver observer)
    {
        _observers.Remove(observer);
    }

}
