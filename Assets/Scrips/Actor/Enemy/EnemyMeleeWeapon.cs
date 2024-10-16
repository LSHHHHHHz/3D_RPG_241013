using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMeleeWeapon : ActorMeleeWeapon<Player>
{
    BaseEnemy baseEnemy;
    private void OnEnable()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
        baseEnemy.onEndEnemyAttackAnim += ResetTarget;
        baseEnemy.onStartEnemyAttackAnim += StartAttackAction;
    }

    private void OnDisable()
    {
        baseEnemy.onEndEnemyAttackAnim -= ResetTarget;
        baseEnemy.onStartEnemyAttackAnim -= StartAttackAction;
    }
    public override void Update()
    {
        targets = ActorManager<Player>.instnace.GetActors();
        base.Update();
    }
}
