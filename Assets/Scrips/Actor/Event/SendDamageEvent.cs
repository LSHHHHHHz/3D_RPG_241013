
using UnityEngine;

public class SendDamageEvent : IEvent
{
    public int damage { get; private set; }
    public Vector3 subjectPos { get; private set; }
    public SendDamageEvent(int damage, Vector3 sub)
    {
        this.damage = damage;
        subjectPos = sub;
    }

    public void SendEvent(IEventReceiver eventReceiver)
    {
        if (eventReceiver != null)
        {
            eventReceiver.ReceiveEvent(this);
        }
    }
}
