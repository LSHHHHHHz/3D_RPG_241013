using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMeleeWeapon : ActorMeleeWeapon<Player>
{
    BaseEnemy baseEnemy;
    private void OnEnable()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }
    private void OnDisable()
    {
    }
    public override void Update()
    {
        base.Update();
        targets = ActorManager<Player>.instnace.GetActors();
    }
}
