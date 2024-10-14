using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class BaseEnemy : Actor
{
    private void OnEnable()
    {
        ActorManager<BaseEnemy>.instnace.RegisterActor(this);
    }
    private void OnDisable()
    {
        ActorManager<BaseEnemy>.instnace.UnregisterActor(this);
    }
}
