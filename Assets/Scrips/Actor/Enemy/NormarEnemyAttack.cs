using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class NormarEnemyAttack : BaseEnemyAttack
{
    public override void AttackAction()
    {
        enemy.fsmController.ChangeState(new EnemyAttackState());
    }
}
