using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Actor : MonoBehaviour, IEventReceiver
{
    public abstract void ReceiveEvent(IEvent ievent);
}
