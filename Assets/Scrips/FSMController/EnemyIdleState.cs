using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<BaseEnemy>
{
    public void Enter(BaseEnemy actor)
    {
    }

    public void Exit(BaseEnemy actor)
    {
    }

    public void Update(BaseEnemy actor)
    {
        if((actor.IsPlayerDetected() || actor.enemyMove.CheckEnemyMove() )&& !actor.IsOriginPos())
        {
            actor.fsmController.ChangeState(new EnemyWalkState());
        }
        if (actor.IsPossibleAttack())
        {
            actor.fsmController.ChangeState(new EnemyAttackState());
        }
    }
}
