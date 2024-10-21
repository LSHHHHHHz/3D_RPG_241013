
public class SendHealingEvent : IEvent
{
    public int amount;
    public SendHealingEvent(int amount)
    {
        this.amount = amount;
    }

    public void SendEvent(IEventReceiver eventReceiver)
    {
        if (eventReceiver != null)
        {
            eventReceiver.ReceiveEvent(this);
        }
    }
}
