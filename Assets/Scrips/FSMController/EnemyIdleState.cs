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
            if (actor is BossEnemy boss)
            {
                int randomValue = Random.Range(0, 100);

                if (randomValue < 60)
                {
                    actor.fsmController.ChangeState(new EnemyWalkState());
                }
                else
                {
                    actor.fsmController.ChangeState(new EnemyApproachPlayerState());
                }
            }
            if(actor is NormarEnemy normar)
            {
                actor.fsmController.ChangeState(new EnemyWalkState());
            }
        }
        if (actor.IsPossibleAttack())
        {
            actor.fsmController.ChangeState(new EnemyAttackState());
        }
    }
}
