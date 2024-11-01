using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : IState<BaseEnemy>
{
    public void Enter(BaseEnemy actor)
    {
        actor.anim.SetBool("IsWalk", true);
    }

    public void Exit(BaseEnemy actor)
    {
        actor.anim.SetBool("IsWalk", false);
    }

    public void Update(BaseEnemy actor)
    {
        if(actor.IsPossibleAttack())
        {
            actor.baseEnemyAttack.AttackAction();
        }
        if(actor.IsOriginPos())
        {
            actor.fsmController.ChangeState(new EnemyIdleState());
        }
    }
}
