using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMeleeWeapon : ActorMeleeWeapon<BaseEnemy>
{
    public override void Update()
    {
        base.Update();
        enemies = ActorManager<BaseEnemy>.instnace.GetActors();
    }
}
