using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<BaseEnemy>
{
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

        if (stateInfo.IsName("Attack"))
        {
            float normalizedTime = stateInfo.normalizedTime % 1;

            if (normalizedTime >= 0.99f)
            {
                actor.fsmController.ChangeState(new EnemyIdleState());
                actor.onStartEnemyAttackAnim?.Invoke(false);
                actor.onEndEnemyAttackAnim?.Invoke();
            }
            else if (normalizedTime >= 0f)
            {
                actor.onStartEnemyAttackAnim?.Invoke(true);
            }
        }
        if (!actor.IsPossibleAttack() && stateInfo.IsName("Attack") && stateInfo.normalizedTime % 1 >= 0.99f)
        {
            actor.fsmController.ChangeState(new EnemyWalkState());
        }
    }
}
