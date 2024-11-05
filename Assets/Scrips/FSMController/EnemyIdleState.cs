using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<NormalEnemy>
{
    public void Enter(NormalEnemy actor)
    {
    }

    public void Exit(NormalEnemy actor)
    {
    }

    public void Update(NormalEnemy actor)
    {
        if ((actor.IsPlayerDetected() || actor.enemyMove.CheckEnemyMove()) && !actor.IsOriginPos())
        {
            actor.fsmController.ChangeState(new EnemyWalkState());
        }
        if (actor.IsPossibleAttack())
        {
            actor.fsmController.ChangeState(new EnemyAttackState());
        }
    }
}
