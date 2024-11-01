using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealState : IState<BaseEnemy>
{
    public void Enter(BaseEnemy actor)
    {
        actor.anim.SetTrigger("DoHeal");
    }
    public void Exit(BaseEnemy actor)
    {
        actor.StartAttack(false);
    }
    public void Update(BaseEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Enemy_HealBuff") && stateInfo.normalizedTime >= 0.95f)
        {
            actor.fsmController.ChangeState(new EnemyIdleState());
        }
    }
}
