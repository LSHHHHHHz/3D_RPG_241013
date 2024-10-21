using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : Actor
{
    public PlayerStatus status { get; private set; }
    private void Awake()
    {
        status = new PlayerStatus(100, 50);
        Debug.Log("Player가 먼저되나");
    }
    private void OnEnable()
    {
        ActorManager<Player>.instnace.RegisterActor(this);
    }
    private void OnDisable()
    {
        ActorManager<Player>.instnace.UnregisterActor(this);
    }

    public override void ReceiveEvent(IEvent ievent)
    {
        if (ievent is SendDamageEvent damageEvent)
        {
            TakeDamage(damageEvent.damage);
        }
        if (ievent is SendHealingEvent healingEvent)
        {
            TakeDamage(healingEvent.amount);
        }
    }
    void TakeDamage(int damage)
    {
        status.ReduceHP(damage);
    }
    void RecoverHP(int amout)
    {
        status.GetHP(amout);
    }
}
