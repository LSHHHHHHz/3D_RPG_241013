using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyAttackState : IState<NormalEnemy>
{
    float isStartAttackTime;
    int loopCount = 0;
    public void Enter(NormalEnemy actor)
    {       
        actor.anim.SetBool("IsAttack", true);
        actor.StartAttack(true);
    }
    public void Exit(NormalEnemy actor)
    {
        actor.anim.SetBool("IsAttack", false);
        actor.StartAttack(false);
    }
    public void Update(NormalEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Enemy_Attack"))
        {
            float normalizedTime = stateInfo.normalizedTime % 1;
            if (normalizedTime >= 0.95f)
            {
                actor.fsmController.ChangeState(new EnemyIdleState());
                actor.onStartEnemyAttackAnim?.Invoke(false);
                actor.onEndEnemyAttackAnim?.Invoke();
            }
            else if (normalizedTime >= isStartAttackTime)
            {
                actor.onStartEnemyAttackAnim?.Invoke(true);
            }
        }       
    }
}
