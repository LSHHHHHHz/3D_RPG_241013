
public class SendDamageEvent : IEvent
{
    public int damage;
    public SendDamageEvent(int damage)
    {
        this.damage = damage;
    }

    public void SendEvent(IEventReceiver eventReceiver)
    {
        if (eventReceiver != null)
        {
            eventReceiver.ReceiveEvent(this);
        }
    }
}
