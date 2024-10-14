using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMeleeWeapon : ActorMeleeWeapon<Player>
{
    public override void Update()
    {
        base.Update();
        enemies = ActorManager<Player>.instnace.GetActors();
    }
}
