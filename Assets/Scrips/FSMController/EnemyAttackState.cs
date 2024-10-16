using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<BaseEnemy>
{
    public event Action onEdnAttackAnim;

    public void Enter(BaseEnemy actor)
    {
        actor.anim.SetBool("IsAttack", true);
        actor.StartAttack(true);
    }
    public void Exit(BaseEnemy actor)
    {
        actor.anim.SetBool("IsAttack", false);
        actor.StartAttack(false);
    }
    public void Update(BaseEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && !actor.IsPossibleAttack())
        {
            if (stateInfo.normalizedTime % 1 >= 0.99f)
            {
                actor.fsmController.ChangeState(new EnemyWalkState());
            }
        }
    }
}
