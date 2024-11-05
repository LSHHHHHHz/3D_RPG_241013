using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : IState<NormalEnemy>
{
    public void Enter(NormalEnemy actor)
    {
        actor.anim.SetBool("IsWalk", true);
    }

    public void Exit(NormalEnemy actor)
    {
        actor.anim.SetBool("IsWalk", false);
    }

    public void Update(NormalEnemy actor)
    {
        if(actor.IsPossibleAttack())
        {
            actor.fsmController.ChangeState(new EnemyAttackState());
        }
        if(actor.IsOriginPos())
        {
            actor.fsmController.ChangeState(new EnemyIdleState());
        }
    }
}
