using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<BaseEnemy>
{
    public void Enter(BaseEnemy actor)
    {
    }

    public void Exit(BaseEnemy actor)
    {
    }

    public void Update(BaseEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Enemy_Attack"))
        {
            if (stateInfo.normalizedTime >= 0.5f && !stateInfo.loop)
            {
                actor.onEndPlayerAttackAnim?.Invoke();
                actor.onStartEnemyAttackAnim?.Invoke(false);
            }
            else if (stateInfo.normalizedTime >= 0)
            {
                actor.onStartEnemyAttackAnim?.Invoke(true);
            }
        }
    }
}
